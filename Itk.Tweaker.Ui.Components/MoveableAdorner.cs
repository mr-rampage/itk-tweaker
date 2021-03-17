using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Itk.Tweaker.Ui.Components
{
    public sealed class MoveableAdorner : Adorner
    {
        public MoveableAdorner(UIElement adornedElement) : base(adornedElement)
        {
            adornedElement.MouseMove += Stage_OnMouseMove;
        }

        private static void Stage_OnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (sender is not DicomStage dicomStage) return;
            if (Mouse.LeftButton is not MouseButtonState.Pressed) return;
            var point = Mouse.GetPosition(dicomStage.Parent as FrameworkElement);
            Canvas.SetTop(dicomStage, point.Y - dicomStage.ActualHeight / 2);
            Canvas.SetLeft(dicomStage, point.X - dicomStage.ActualWidth / 2);
        }

    }
}