using System.Windows;
using System.Windows.Controls;

namespace Itk.Tweaker.Ui.Components
{
    public static class UiElementExtensions
    {
        public static void RemoveChild(this DependencyObject parent, UIElement child)
        {
            switch (parent)
            {
                case Panel panel :
                    panel.Children.Remove(child);
                    break;
                case Decorator decorator when decorator.Child == child:
                    decorator.Child = null;
                    break;
                case ContentPresenter contentPresenter when contentPresenter.Content == child:
                    contentPresenter.Content = null;
                    break;
                case ContentControl contentControl when contentControl.Content == child:
                    contentControl.Content = null;
                    break;
            }
        }
    }
}