using System;
using System.Windows;
using System.Windows.Controls;
using SorterControls.ViewModels.Bulders;

namespace SorterControls.Views.Builders
{
    public class StepBuilderTemplateSelector : DataTemplateSelector
    {
        #region  CompetePoolStepBuilderTemplate

        public DataTemplate CompetePoolStepBuilderTemplate { get; set; }

        #endregion

        #region  EmptyStepBuilderTemplate

        public DataTemplate EmptyStepBuilderTemplate { get; set; }

        #endregion

        #region  SwitchablePoolStepBuilderTemplate

        public DataTemplate SwitchablePoolStepBuilderTemplate { get; set; }

        #endregion

        #region  SorterPoolStepBuilderTemplate

        public DataTemplate SorterPoolStepBuilderTemplate { get; set; }

        #endregion

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var tabItem = item as IWorkflowStepBuilderVm;

            if (tabItem != null)
                switch (tabItem.TypeName)
                {
                    case CompetePoolBuilderVm.TemplateName:
                        return CompetePoolStepBuilderTemplate;
                    case SwitchablePoolBuilderVm.TemplateName:
                        return SwitchablePoolStepBuilderTemplate;
                    case SorterPoolBuilderVm.TemplateName:
                        return SorterPoolStepBuilderTemplate;
                    case EmptyStepBuilderVm.TemplateName:
                        return EmptyStepBuilderTemplate;
                    default:
                        throw new Exception("Unhandled IWorkflowStepBuilderVm template name");
                }
            return EmptyStepBuilderTemplate;
            //throw new Exception("TypeName is null in StepBuilderTemplateSelector.SelectTemplate");
        }

    }
}
