module Pipeline.Itk.ItkImage

type internal AnatomicalPlane =
    | Sagittal
    | Coronal
    | Transverse

let internal PlaneToVectorIndex axis =
    match axis with
    | AnatomicalPlane.Sagittal -> 0
    | AnatomicalPlane.Coronal -> 1
    | AnatomicalPlane.Transverse -> 2
    
type internal Dimension2D<'T> = { Width: 'T; Height: 'T }

type DicomImage<'T> = {
    Image: 'T
    Thumbnail: 'T
}