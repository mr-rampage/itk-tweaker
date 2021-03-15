module Pipeline.Itk.ItkImageLoader

open System.IO
open itk.simple

let private LoadUsingItk (dicomPath: DirectoryInfo) =
    try
        use imageSeriesReader = new ImageSeriesReader()
        use fileNames = ImageSeriesReader.GetGDCMSeriesFileNames dicomPath.FullName
        imageSeriesReader.SetFileNames fileNames
        imageSeriesReader.SetOutputPixelType PixelIDValueEnum.sitkInt32
        Ok(imageSeriesReader.Execute())
    with
    | e -> Error e
    
let Load = LoadUsingItk