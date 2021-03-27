module Pipeline.Itk.ItkPipeline

open Pipeline.Core.Pipeline
open Pipeline.Itk.ItkGaussian
open Pipeline.Itk.ItkImageLoader
open itk.simple

type AddStageEvent =
    | Identity
    | SmoothingRecursiveGaussianImageFilter of sigma: double
    
let private AdaptAddStageEvent (event: AddStageEvent) =
    match (event) with
    | Identity -> id
    | SmoothingRecursiveGaussianImageFilter σ -> ApplyGaussianBlur2D σ

let AddImageTransformStage = AddStage AdaptAddStageEvent

let CreateThumbnails sourceImage (pipeline: Pipeline<Image>) =
    sourceImage
    |> CreateThumbnail
    |> Result.map (RunPipeline pipeline)
