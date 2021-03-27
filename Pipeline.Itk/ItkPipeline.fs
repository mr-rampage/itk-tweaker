module Pipeline.Itk.ItkPipeline

open Pipeline.Core.Pipeline
open Pipeline.Itk.ItkGaussian
open itk.simple

type AddStageEvent =
    | Identity of unit
    | GaussianBlur of sigma: double
    
let private AdaptAddStageEvent (event: AddStageEvent) =
    match (event) with
    | Identity () -> id
    | GaussianBlur σ -> ApplyGaussianBlur2D σ

let AddStage (event: AddStageEvent) (pipeline: Pipeline<Image>) =
    let updatedPipeline = event |> AdaptAddStageEvent |> AddTransform pipeline
    (updatedPipeline, List.last updatedPipeline)

let CreatePipeline () = [id]
