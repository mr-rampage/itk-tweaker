module Pipeline.Core.Pipeline

open Pipeline.Core.Combinators

type Stage<'I, 'O> = ('I -> 'O)

type TransformStage<'T> = Stage<'T, 'T>

type Pipeline<'T> = TransformStage<'T> list

type EventAdapter<'E, 'T> = ('E -> TransformStage<'T>)

let private AddTransform (stage: TransformStage<'T>) (pipeline: Pipeline<'T>) =
    if (pipeline.IsEmpty)
    then [ stage ]
    else pipeline @ [ (List.last pipeline) >> stage ]

let AddStage (adapt: EventAdapter<'E, 'T>) e = e |> adapt |> AddTransform

let RunPipeline pipeline x = List.map (Thrush x) pipeline
