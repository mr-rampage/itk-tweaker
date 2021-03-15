module Pipeline.Core.ImagePipelines

let ResampleImage2D sliceImage resizeImage image =
    image
    |> sliceImage
    |> Result.bind resizeImage