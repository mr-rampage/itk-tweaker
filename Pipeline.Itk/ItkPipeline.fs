module Pipeline.Itk.ItkPipeline

open Pipeline.Core.Pipeline
open Pipeline.Itk.ItkGaussian
open Pipeline.Itk.ItkImageLoader
open itk.simple

type AddStageEvent =
    | SourceStage of unit
    | GaussianBlur of sigma: double
    
let private AdaptAddStageEvent (event: AddStageEvent) =
    match (event) with
    | SourceStage () -> id
    | GaussianBlur σ -> ApplyGaussianBlur2D σ


let AddImageTransformStage (event: AddStageEvent) (pipeline: Pipeline<Image>) =
    AddStage AdaptAddStageEvent event pipeline

let CreatePipeline () = [id]

let CreateThumbnails sourceImage (pipeline: Pipeline<Image>) =
    sourceImage
    |> CreateThumbnail
    |> Result.map (RunPipeline pipeline)
