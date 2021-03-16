module Pipeline.Itk.ItkImagePipelines

open Pipeline.Itk.ItkSlice
open Pipeline.Itk.ItkResize
open itk.simple

let SelectMidSlice axis image =
    let midSlice = int(CountSlices axis image) / 2
    ItkSlice2D axis midSlice image

let CreateThumbnail axis size (imageResult: Result<Image, exn>) =
    imageResult
      |> Result.bind (SelectMidSlice axis)
      |> Result.bind (ItkResize2D size)