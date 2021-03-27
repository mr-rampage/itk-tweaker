using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Itk.Tweaker.Ui.Components;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using Pipeline.Itk;
using Image = itk.simple.Image;

namespace Itk.Tweaker.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private FSharpList<FSharpFunc<Image, Image>> _pipeline;

        public MainWindow()
        {
            InitializeComponent();
            _pipeline = ItkPipeline.CreatePipeline<Image>();
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
                        var (pipeline, transform) = 
                            ItkPipeline.AddStage(ItkPipeline.AddStageEvent.NewGaussianBlur(2), _pipeline);
                        _pipeline = pipeline;
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