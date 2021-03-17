﻿module Pipeline.Itk.ItkSlice

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
    let sliceIndex = new VectorInt32()
    sliceIndex.Add(0)
    sliceIndex.Add(0)
    sliceIndex.Add(0)
    sliceIndex.[AxisToVectorIndex(axis)] <- index
    sliceIndex
    
let internal ItkSlice2D axis index (image: Image) =
    let size = SliceDimensions axis image
    let index = SliceIndex axis index
    Ok(SimpleITK.Extract(image,size,index))
