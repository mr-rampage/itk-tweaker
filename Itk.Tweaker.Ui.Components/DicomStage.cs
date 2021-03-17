using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Itk.Tweaker.Ui.Components
{
    public interface IDicomStage
    {
    }

    public class DicomStage : ContentControl, IDicomStage
    {
        protected DicomStage()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            MouseMove += Stage_OnMouseMove;
            MouseEnter += Stage_OnMouseEnter;
            MouseLeave += Stage_OnMouseLeave;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            MouseMove -= Stage_OnMouseMove;
            MouseEnter -= Stage_OnMouseEnter;
            MouseLeave -= Stage_OnMouseLeave;
        }

        public enum State
        {
            Unselected,
            Selected
        }

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register(nameof(Status), typeof(State), typeof(DicomStage),
                new PropertyMetadata(State.Unselected));

        public State Status
        {
            get => (State) GetValue(StatusProperty);
            protected set => SetValue(StatusProperty, value);
        }

        private static void Stage_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is not DicomStage dicomStage) return;
            if (Mouse.LeftButton is not MouseButtonState.Pressed) return;
            var point = Mouse.GetPosition(dicomStage.Parent as FrameworkElement);
            Canvas.SetTop(dicomStage, point.Y - dicomStage.ActualHeight / 2);
            Canvas.SetLeft(dicomStage, point.X - dicomStage.ActualWidth / 2);
        }

        private static void Stage_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is DicomStage dicomStage)
                dicomStage.Status = State.Selected;
        }

        private static void Stage_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is DicomStage dicomStage)
                dicomStage.Status = State.Unselected;
        }

        protected void AddPipelineStage(object sender, RoutedEventArgs e) 
            => RaiseEvent(new AddPipelineStageEvent());

        protected void RemoveStage(object sender, RoutedEventArgs e) 
            => RaiseEvent(new RemovePipelineStageEvent());
    }
}