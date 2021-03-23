module Pipeline.Itk.ItkImageLoader

open System.IO
open Pipeline.Itk.ItkImage
open Pipeline.Itk.ItkSlice
open Pipeline.Itk.ItkResize
open itk.simple

let internal LoadDicomFromFolder(dicomPath: DirectoryInfo) =
    try
        use imageSeriesReader = new ImageSeriesReader()
        use fileNames = ImageSeriesReader.GetGDCMSeriesFileNames dicomPath.FullName
        imageSeriesReader.SetFileNames fileNames
        imageSeriesReader.SetOutputPixelType PixelIDValueEnum.sitkInt32
        Ok(imageSeriesReader.Execute())
    with
    | e -> Error e
    
let internal CreateDicomImage image =
    image
    |> GetMedianSlice AnatomicalPlane.Transverse
    |> Result.bind (ItkResize2D 256)
    |> Result.map (fun thumbnail -> { Image = image; Thumbnail = thumbnail })

let LoadImage (dicomFolder: DirectoryInfo) =
    dicomFolder
    |> LoadDicomFromFolder
    |> Result.bind CreateDicomImage