using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using itk.simple;
using Pipeline.Itk;

namespace Itk.Tweaker.Ui.Components
{
    public partial class DicomImage
    {
        public DicomImage()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItkImageProperty =
            DependencyProperty.Register(
                nameof(ItkImage), typeof(Image),
                typeof(DicomImage)
            );

        public Image ItkImage
        {
            get => (Image)GetValue(ItkImageProperty);
            set => SetValue(ItkImageProperty, value);
        }
        
        private async void OnDicomImageSelected(object sender, RoutedEventArgs e)
        {
            if (sender is not SelectImageButton selectImageButton) return;

            var dicomFolder = selectImageButton.SelectedFolder;
            var dicom = await Task.Run(() => ItkImagePipelines.LoadImage(dicomFolder));

            if (dicom.IsError) return;
            ItkImage = dicom.ResultValue.Image;
            ImageThumbnail.Source = dicom.ResultValue.Thumbnail.AsImageSource(PixelFormats.Bgr32);
        }
    }
}