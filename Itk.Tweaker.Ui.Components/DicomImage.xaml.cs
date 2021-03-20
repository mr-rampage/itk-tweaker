using System.Windows;
using System.Windows.Media;

namespace Itk.Tweaker.Ui.Components
{
    public partial class DicomImage
    {
        public DicomImage()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ThumbnailProperty=
            DependencyProperty.Register(
                nameof(Thumbnail), typeof(ImageSource),
                typeof(DicomImage)
            );

        public ImageSource Thumbnail
        {
            get => (ImageSource)GetValue(ThumbnailProperty);
            set => SetValue(ThumbnailProperty, value);
        }

        private void OnDicomImageSelected(object sender, RoutedEventArgs e)
        {
            if (sender is not SelectImageButton selectImageButton) return;
            var dicomFolder = selectImageButton.SelectedFolder;
            if (!dicomFolder.Exists) return;
            RaiseEvent(new LoadDicomImageEvent(dicomFolder));
        }
    }
}