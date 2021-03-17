using System.Collections.ObjectModel;
using System.Windows;
using Itk.Tweaker.Ui.Components;

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

        private void HandlePipelineEvents(object sender, PipelineEventArg e)
        {
            switch (e)
            {
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