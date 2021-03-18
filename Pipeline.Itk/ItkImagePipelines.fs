module Pipeline.Itk.ItkImagePipelines

open System.IO
open Pipeline.Itk.ItkImage
open Pipeline.Itk.ItkImageLoader
open Pipeline.Itk.ItkSlice
open Pipeline.Itk.ItkResize

let internal CreateDicomImage image =
    image
    |> ExtractMidSlice Axis.Z
    |> Result.bind (ItkResize2D 256)
    |> Result.map (fun thumbnail -> { Image = image; Thumbnail = thumbnail })

let LoadImage (dicomFolder: DirectoryInfo) =
    dicomFolder
    |> LoadDicomFromFolder
    |> Result.bind CreateDicomImage