module Pipeline.Itk.ItkResize

open Pipeline.Itk.ItkImage
open itk.simple

exception InvalidDimensionsError of string

let internal GetResampledDimensions (outputSize: int) (image: Image) =
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

let private ItkResize (newDimensions: Dimension2D<double>*Dimension2D<double>) (image: Image) =
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
    
let internal ItkResize2D outputSize (image: Image) =
    let dimensions = image.GetDimension()
    if dimensions = uint32(2) then
       Ok (ItkResize (GetResampledDimensions outputSize image) image)
    else
       Error (InvalidDimensionsError $"The dimensions {dimensions} is not supported for 2D resampling")
