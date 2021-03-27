using System.Windows;
using System.Windows.Media;

namespace Itk.Tweaker.Ui.Components
{
    public partial class DicomTransformStage
    {
        public DicomTransformStage()
        {
            InitializeComponent();
        }
        
        public static readonly DependencyProperty ThumbnailProperty=
            DependencyProperty.Register(
                nameof(Thumbnail), typeof(ImageSource),
                typeof(DicomTransformStage)
            );

        public ImageSource Thumbnail
        {
            get => (ImageSource)GetValue(ThumbnailProperty);
            set => SetValue(ThumbnailProperty, value);
        }
    }
}