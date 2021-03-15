using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using itk.simple;

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
            private set => SetValue(ItkImageProperty, value);
        }
        
        private async void OnDicomImageSelected(object sender, RoutedEventArgs e)
        {
            if (sender is not SelectImageButton selectImageButton) return;

            var dicomFolder = selectImageButton.SelectedFolder;
            var dicom = await IDicom<Image>.Load(dicomFolder);

            ItkImage = dicom?.Image;
            ImageThumbnail.Source = dicom?.Thumbnail?.AsImageSource(PixelFormats.Bgr32);
        }
    }
}