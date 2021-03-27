module Pipeline.Itk.ItkGaussian

open itk.simple

let ApplyGaussianBlur2D σ image =
    let sigma = new VectorDouble([σ;σ])
    SimpleITK.SmoothingRecursiveGaussian(image, sigma)