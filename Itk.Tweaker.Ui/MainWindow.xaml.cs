using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Itk.Tweaker.Ui.Components;
using Pipeline.Itk;

namespace Itk.Tweaker.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Pipeline.Add(new DicomSourceStage());
        }

        private async void HandlePipelineEvents(object sender, PipelineEventArg e)
        {
            switch (e)
            {
                case LoadDicomImageEvent loadDicomImageEvent when e.OriginalSource is DicomImage dicomImage:
                    var dicom = await Task.Run(() => ItkImageLoader.LoadImage(loadDicomImageEvent.DicomPath));
                    if (dicom.IsError) return;
                    dicomImage.Thumbnail = dicom.ResultValue.Thumbnail.AsImageSource(PixelFormats.Bgr32);
                    break;
                case AddPipelineStageEvent:
                    var transformStage = new DicomTransformStage();
                    Pipeline.Add(transformStage);
                    break;
                case RemovePipelineStageEvent when e.OriginalSource is IDicomStage stage:
                    Pipeline.Remove(stage);
                    break;
            }
        }

        public ObservableCollection<IDicomStage> Pipeline { get; } = new();
    }
}