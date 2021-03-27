using System.IO;
using System.Windows;

namespace Itk.Tweaker.Ui.Components
{
    public delegate void PipelineEventHandler(object sender, PipelineEventArg e);

    public static class PipelineAction
    {
        public static readonly RoutedEvent PipelineActionEvent = EventManager.RegisterRoutedEvent(nameof(PipelineAction),
            RoutingStrategy.Bubble, typeof(PipelineEventHandler), typeof(PipelineAction));
        
        public static void AddPipelineActionHandler(DependencyObject dependencyObject, PipelineEventHandler routedEventHandler)
        {
            if (dependencyObject is UIElement uiElement)
            {
                uiElement.AddHandler(PipelineActionEvent, routedEventHandler);
            }
        }
        
        public static void RemovePipelineActionHandler(DependencyObject dependencyObject, PipelineEventHandler routedEventHandler)
        {
            if (dependencyObject is UIElement uiElement)
            {
                uiElement.RemoveHandler(PipelineActionEvent, routedEventHandler);
            }
        }
    }

    public abstract class PipelineEventArg: RoutedEventArgs
    {

        internal PipelineEventArg() : base(PipelineAction.PipelineActionEvent)
        {
        }
    }

    public sealed class AddPipelineStageEvent : PipelineEventArg
    {
        
    }
    
    public sealed class RemovePipelineStageEvent : PipelineEventArg
    {
        
    }

    public sealed class LoadDicomImageEvent : PipelineEventArg
    {
        public DirectoryInfo DicomPath { get; }

        internal LoadDicomImageEvent(DirectoryInfo dicomPath)
        {
            DicomPath = dicomPath;
        }
    }
}