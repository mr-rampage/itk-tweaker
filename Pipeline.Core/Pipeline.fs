module Pipeline.Core.Pipeline

open Pipeline.Core.Combinators

type Stage<'I, 'O> = ('I -> 'O)

type TransformStage<'T> = Stage<'T, 'T>

type Pipeline<'T> = TransformStage<'T> list

let AddTransform (pipeline: Pipeline<'T>) (stage: TransformStage<'T>) =
    if (pipeline.IsEmpty)
    then [ stage ]
    else (pipeline.Head >> stage) :: pipeline

let RunPipeline pipeline x = List.map (Thrush x) pipeline
