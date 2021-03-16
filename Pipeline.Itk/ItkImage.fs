module Pipeline.Itk.ItkImage

type Axis =
    | X
    | Y
    | Z

let AxisToVectorIndex axis =
    match axis with
    | Axis.X -> 0
    | Axis.Y -> 1
    | Axis.Z -> 2
    
type internal Dimension2D<'T> = { Width: 'T; Height: 'T }
