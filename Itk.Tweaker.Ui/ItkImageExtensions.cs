using System;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using itk.simple;

namespace Itk.Tweaker.Ui
{
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