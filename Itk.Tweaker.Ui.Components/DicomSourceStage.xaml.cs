using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Itk.Tweaker.Ui.Components
{
    public partial class DicomSourceStage
    {
        public enum State
        {
            Unselected, Selected
        }
        public DicomSourceStage()
        {
            InitializeComponent();
        }
        
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register(nameof(Status), typeof(State), typeof(DicomSourceStage), 
                new PropertyMetadata(State.Unselected));

        public State Status 
        {
            get => (State)GetValue(StatusProperty);
            private set => SetValue(StatusProperty, value);
        }

        private void RemoveStage(object sender, RoutedEventArgs e)
        {
            Parent.RemoveChild(this);
        }

        private void Stage_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton is not MouseButtonState.Pressed) return;
            var point = Mouse.GetPosition(Parent as FrameworkElement);
            Canvas.SetTop(this, point.Y - ActualHeight / 2);
            Canvas.SetLeft(this, point.X - ActualWidth / 2);
        }

        private void Stage_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Status = State.Selected;
        }

        private void Stage_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Status = State.Unselected;
        }
    }
}