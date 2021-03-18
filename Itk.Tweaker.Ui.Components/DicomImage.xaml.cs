using System.Windows;
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
            set => SetValue(ItkImageProperty, value);
        }
        
        private async void OnDicomImageSelected(object sender, RoutedEventArgs e)
        {
            if (sender is not SelectImageButton selectImageButton) return;

            var dicomFolder = selectImageButton.SelectedFolder;
            var dicom = await IDicom.Load(dicomFolder);

            if (dicom is not ValidDicom validDicom) return;
            ItkImage = validDicom.Image;
            ImageThumbnail.Source = validDicom.Thumbnail.AsImageSource(PixelFormats.Bgr32);
        }
    }
}