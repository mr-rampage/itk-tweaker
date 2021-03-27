module Pipeline.Itk.ItkSlice

open Pipeline.Core.Combinators
open Pipeline.Itk.ItkImage
open itk.simple

let internal CountSlices plane (image: Image) =
    match plane with
    | AnatomicalPlane.Sagittal -> image.GetWidth()
    | AnatomicalPlane.Coronal -> image.GetHeight()
    | AnatomicalPlane.Transverse -> image.GetDepth()
    
let private SliceDimensions plane (image: Image) =
    let size = image.GetSize()
    size.[PlaneToVectorIndex(plane)] <- uint32(0)
    size
    
let private SliceIndex plane index =
    let sliceIndex = new VectorInt32([0;0;0])
    sliceIndex.[PlaneToVectorIndex(plane)] <- index
    sliceIndex
    
let internal ItkSlice2D plane index (image: Image) =
    let size = SliceDimensions plane image
    let index = SliceIndex plane index
    Ok(SimpleITK.Extract(image,size,index))

let private DivideBy denominator numerator = int(numerator) / denominator
let private MedianIndex plane = (CountSlices plane) >> (DivideBy 2)
let internal GetMedianSlice plane = Converge Chain ItkSlice2D MedianIndex plane
