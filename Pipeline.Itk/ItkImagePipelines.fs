module Pipeline.Itk.ItkImagePipelines

open Pipeline.Core.ImagePipelines
open itk.simple

type Axis =
    | X
    | Y
    | Z
    
let private CountSlices axis (image: Image) =
    match axis with
    | Axis.X -> int32(image.GetWidth())
    | Axis.Y -> int32(image.GetHeight())
    | Axis.Z -> int32(image.GetDepth())
    
let private SliceDimensions axis (image: Image) =
    let size = image.GetSize()
    match axis with
    | Axis.X -> size.[0] <- uint32(0)
    | Axis.Y -> size.[1] <- uint32(0)
    | Axis.Z -> size.[2] <- uint32(0)
    size
    
let private SliceIndex axis index =
    let sliceIndex = new VectorInt32()
    sliceIndex.Add(0)
    sliceIndex.Add(0)
    sliceIndex.Add(0)
    match axis with
    | Axis.X -> sliceIndex.[0] <- index
    | Axis.Y -> sliceIndex.[1] <- index
    | Axis.Z -> sliceIndex.[2] <- index
    sliceIndex
    
let private ItkSlice axis index (image: Image) =
    let size = SliceDimensions axis image
    let index = SliceIndex axis index
    Ok(SimpleITK.Extract(image,size,index))
    
exception InvalidDimensionsError of string

type Dimension2D<'T> = { Width: 'T; Height: 'T }

let private GetResampledDimensions (outputSize: int) (image: Image) =
    let inputSize = image.GetSize();
    let inputSpacing = image.GetSpacing()
    let oldSize = {
        Width = double(inputSize.[0]) * inputSpacing.[0];
        Height = double(inputSize.[1]) * inputSpacing.[1];
    }
    let scaling = {
        Width = oldSize.Width / double(outputSize);
        Height = oldSize.Height / double(outputSize);
    }
    let newSize = {
        Width = if scaling.Width > scaling.Height then double(outputSize) else oldSize.Width / scaling.Height;
        Height = if scaling.Width > scaling.Height then oldSize.Height / scaling.Width else double(outputSize);
    }
    let newSpacing = {
        Width = oldSize.Width / newSize.Width
        Height = oldSize.Height / newSize.Height
    }
    (newSpacing, newSize)

let private ItkResize newDimensions (image: Image) =
    let (spacing, size) = newDimensions
    
    let newSpacing = new VectorDouble()
    newSpacing.Add(double(spacing.Width))
    newSpacing.Add(double(spacing.Height))
    
    let actualSize = new VectorUInt32()
    actualSize.Add(uint32(size.Width))
    actualSize.Add(uint32(size.Height))
    
    use filter = new ResampleImageFilter()
    filter.SetReferenceImage image
    filter.SetSize actualSize
    filter.SetOutputSpacing newSpacing
    filter.Execute(image)
    
    
let private ItkResize2D outputSize (image: Image) =
    let dimensions = image.GetDimension()
    if dimensions = uint32(2) then
       Ok (ItkResize (GetResampledDimensions outputSize image) image)
    else
       Error (InvalidDimensionsError $"The dimensions {dimensions} is not supported for 2D resampling")

    
let CreateThumbnail axis size (imageResult: Result<Image, exn>) =
    match imageResult with
    | Ok image ->
        ResampleImage2D
            (ItkSlice axis ((CountSlices axis image) / 2))
            (ItkResize2D size)
            image
    | e -> e