using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicModel.Model;
using DynamicModel.ViewModel;
using WpfUtils;

namespace SorterControls.ViewModels.Bulders
{
    public class EmptyStepBuilderVm : StepBuilderVm
    {
        public EmptyStepBuilderVm()
            : base(null)
        {
        }

        public const string TemplateName = "EmptyPoolStepBuilder";

        public override CommandViewModel BuildCommandVm
        {
            get { throw new NotImplementedException();  }
        }

        public override string TypeName
        {
            get { return TemplateName; }
        }
    }
}
