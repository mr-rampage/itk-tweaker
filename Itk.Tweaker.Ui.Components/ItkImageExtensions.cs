using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using itk.simple;
using Pipeline.Itk;

namespace Itk.Tweaker.Ui.Components
{
    public interface IDicom<out T>
    {
        T Image { get; }
        T Thumbnail { get; }
        
        public static async Task<IDicom<Image>> Load(DirectoryInfo dicomFolder)
        {
            if (!dicomFolder.Exists) return new ItkDicom(null, null);

            var (dicom, dicomThumbnail) = await Task.Run(() =>
            {
                var image = ItkImageLoader.Load.Invoke(dicomFolder);
                var thumbnail = ItkImagePipelines.
                    CreateThumbnail(ItkImage.Axis.Z, 256, image);

                return (
                    image.IsOk ? image.ResultValue : null,
                    thumbnail.IsOk ? thumbnail.ResultValue : null
                );
            });
            return new ItkDicom(dicom, dicomThumbnail);
        }
    }
    
    public record ItkDicom(Image Image, Image Thumbnail) : IDicom<Image>;
    
    
    public static class ItkImageExtensions
    {
        public static ImageSource AsImageSource(this Image image, PixelFormat pf)
        {
            var rawStride = (int) image.GetWidth() * sizeof(int);
            return BitmapSource.Create(
                (int) image.GetWidth(), (int) image.GetHeight(),
                96, 96,
                pf, null,
                AsPixelArray(image),
                rawStride
            );
        }

        private static int[] AsPixelArray(Image image)
        {
            var copy = SimpleITK.Cast(image, PixelIDValueEnum.sitkInt32);
            var length = Convert.ToInt32(copy.GetNumberOfPixels());
            var buffer = copy.GetConstBufferAsInt32();
            var bufferAsArray = new int[length * sizeof(int)];
            Marshal.Copy(buffer, bufferAsArray, 0, length);
            return bufferAsArray;
        }
    }
}