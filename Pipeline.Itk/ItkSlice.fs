module Pipeline.Itk.ItkSlice

open Pipeline.Itk.Combinators
open Pipeline.Itk.ItkImage
open itk.simple

let internal CountSlices axis (image: Image) =
    match axis with
    | Axis.X -> image.GetWidth()
    | Axis.Y -> image.GetHeight()
    | Axis.Z -> image.GetDepth()
    
let private SliceDimensions axis (image: Image) =
    let size = image.GetSize()
    size.[AxisToVectorIndex(axis)] <- uint32(0)
    size
    
let private SliceIndex axis index =
    let sliceIndex = new VectorInt32([0;0;0])
    sliceIndex.[AxisToVectorIndex(axis)] <- index
    sliceIndex
    
let internal ItkSlice2D axis index (image: Image) =
    let size = SliceDimensions axis image
    let index = SliceIndex axis index
    Ok(SimpleITK.Extract(image,size,index))

let private DivideBy denominator numerator = int(numerator) / denominator
let private MidSliceIndex axis = Pipe (CountSlices axis) (DivideBy 2)
let internal ExtractMidSlice axis = Converge Chain ItkSlice2D MidSliceIndex axis
