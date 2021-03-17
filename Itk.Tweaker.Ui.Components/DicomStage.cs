using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Image = itk.simple.Image;

namespace Itk.Tweaker.Ui.Components
{
    public interface IDicomStage
    {
        Image Image { get; }
    }

    public class DicomStage : ContentControl, IDicomStage
    {
        protected DicomStage()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer
                .GetAdornerLayer(this)
                ?.Add(new MoveableAdorner(this));
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register(nameof(Image), typeof(Image), typeof(DicomStage));

        public Image Image
        {
            get => (Image) GetValue(ImageProperty);
            protected set => SetValue(ImageProperty, value);
        }

        protected void AddPipelineStage(object sender, RoutedEventArgs e) 
            => RaiseEvent(new AddPipelineStageEvent());

        protected void RemoveStage(object sender, RoutedEventArgs e) 
            => RaiseEvent(new RemovePipelineStageEvent());
    }
}