using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Controls;
using Image = itk.simple.Image;

namespace Itk.Tweaker.Ui
{
    public class DataflowBlockDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ImageSourceTemplate { get; set; }

        public DataTemplate ImageTransformTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) =>
            item switch
            {
                BufferBlock<Image> => ImageSourceTemplate,
                TransformBlock<Image, Image> => ImageTransformTemplate,
                _ => null
            };
    }
}