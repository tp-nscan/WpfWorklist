using System;
using System.Windows;
using System.Windows.Controls;
using DynamicModel.ViewModel;
using SorterControls.ViewModels.Bulders;
using SorterControls.ViewModels.Steps;

namespace SorterControls.Views.Entities
{
    public class EntityTemplateSelector : DataTemplateSelector
    {
        #region  SwitchablePoolTemplate

        public DataTemplate SwitchablePoolTemplate { get; set; }

        #endregion

        #region  SorterPoolTemplate

        public DataTemplate SorterPoolTemplate { get; set; }

        #endregion

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var tabItem = item as IStepVm;
            if (item == null)
            {
                return null;
            }

            return SorterPoolTemplate;


            //if (tabItem != null)
            //    switch (tabItem.TypeName)
            //    {
            //        case CompetePoolBuilderVm.TemplateName:
            //            return CompetePoolTemplate;
            //        case SwitchablePoolBuilderVm.TemplateName:
            //            return SwitchablePoolTemplate;
            //        case SorterPoolBuilderVm.TemplateName:
            //            return SorterPoolTemplate;
            //        default:
            //            throw new Exception("Unhandled IWorkflowStepBuilderVm template name");
            //    }

            //throw new Exception("TypeName is null in ModelStepTemplateSelector.SelectTemplate");
        }

    }
}
