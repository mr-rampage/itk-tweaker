module Pipeline.Itk.ItkImage

type internal Axis =
    | X
    | Y
    | Z

let internal AxisToVectorIndex axis =
    match axis with
    | Axis.X -> 0
    | Axis.Y -> 1
    | Axis.Z -> 2
    
type internal Dimension2D<'T> = { Width: 'T; Height: 'T }

type DicomImage<'T> = {
    Image: 'T
    Thumbnail: 'T
}