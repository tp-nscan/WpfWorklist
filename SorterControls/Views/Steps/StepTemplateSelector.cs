using System;
using System.Windows;
using System.Windows.Controls;
using DynamicModel.ViewModel;
using SorterControls.ViewModels.Steps;

namespace SorterControls.Views.Steps
{
    public class StepTemplateSelector : DataTemplateSelector
    {
        #region  CompetePoolStepTemplate

        public DataTemplate CompetePoolStepTemplate { get; set; }

        #endregion

        #region  EmptyStepTemplate

        public DataTemplate EmptyStepTemplate { get; set; }

        #endregion

        #region  SwitchablePoolStepTemplate

        public DataTemplate SwitchablePoolStepTemplate { get; set; }

        #endregion

        #region  SorterPoolStepTemplate

        public DataTemplate SorterPoolStepTemplate { get; set; }

        #endregion

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var tabItem = item as IStepVm;
            if (tabItem == null)
            {
                return null;
            }

            switch (tabItem.TypeName)
            {
                case CompetePoolStepVmImpl.TemplateName:
                    return CompetePoolStepTemplate;
                case SwitchablePoolStepVmImpl.TemplateName:
                    return SwitchablePoolStepTemplate;
                case SorterPoolStepVmImpl.TemplateName:
                    return SorterPoolStepTemplate;
                case EmptyStepVmImpl.TemplateName:
                    return EmptyStepTemplate;
                default:
                    throw new Exception("Unhandled DataTemplateSelector template name");
            }
        }
    }
}
