﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Itk.Tweaker.Ui.Components
{
    public interface IDicomStage
    {
    }

    public class DicomStage : ContentControl, IDicomStage
    {
        protected DicomStage()
        {
        }

        protected void AddPipelineStage(object sender, RoutedEventArgs e) 
            => RaiseEvent(new AddPipelineStageEvent());

        protected void RemoveStage(object sender, RoutedEventArgs e) 
            => RaiseEvent(new RemovePipelineStageEvent());
    }
}