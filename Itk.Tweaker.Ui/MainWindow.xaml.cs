using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Media;
using Itk.Tweaker.Ui.Components;
using Pipeline.Itk;
using Image = itk.simple.Image;

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
        }

        public ObservableCollection<ImageSource> Pipeline { get; } = new();
        
        private Image SourceImage { get; set; }

        private async void HandlePipelineEvents(object sender, PipelineEventArg e)
        {
            switch (e)
            {
                case LoadDicomImageEvent loadDicomImageEvent:
                    var dicom = await Task.Run(() => ItkImageLoader.LoadImage(loadDicomImageEvent.DicomPath));
                    if (dicom.IsError) return;
                    SourceImage = dicom.ResultValue.Image;
                    Pipeline.Add(dicom.ResultValue.Thumbnail.AsImageSource(PixelFormats.Bgr32));
                    break;
                case AddPipelineStageEvent:
                    var thumbnail = await Task.Run(() =>
                    {
                        var transform = ItkPipeline.AddStage(ItkPipeline.AddStageEvent.NewGaussianBlur(5));
                        return transform.Invoke(ItkImageLoader.CreateThumbnail(SourceImage).ResultValue);
                    });
                    Pipeline.Add(thumbnail.AsImageSource(PixelFormats.Bgr32));
                    break;
                case RemovePipelineStageEvent when e.OriginalSource is IDicomStage stage:
                    break;
            }
        }
    }
}