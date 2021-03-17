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
            if (sender is not FrameworkElement element) return;
            if (Mouse.LeftButton is not MouseButtonState.Pressed) return;
            var point = Mouse.GetPosition(element.Parent as FrameworkElement);
            Canvas.SetTop(element, point.Y - element.ActualHeight / 2);
            Canvas.SetLeft(element, point.X - element.ActualWidth / 2);
        }

    }
}