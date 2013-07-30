using System;
using EquationEditor.Models.Operator;
using WpfUtils;

namespace EquationEditor.ViewModels.Operator
{
    public class OperatorsVm : ViewModelBase
    {
        private ObservableCollectionEx<OperatorVm> _standardOperatorVms = new ObservableCollectionEx<OperatorVm>();
        private ObservableCollectionEx<OperatorVm> _macroOperatorVms = new ObservableCollectionEx<OperatorVm>();

        public OperatorsVm(IOperatorRepository operatorRepository)
        {
            _operatorRepository = operatorRepository;

            foreach (var oper in _operatorRepository.Operators)
            {
                switch (oper.StandardOrMacroOperator)
                {
                    case StandardOrMacroOperator.Standard:
                        StandardOperatorVms.Add(new OperatorVm(oper, oper.Name));
                        break;
                    case StandardOrMacroOperator.Macro:
                        MacroOperatorVms.Add(new OperatorVm(oper, oper.Name));
                        break;
                    default:
                        throw new Exception(string.Format("{0}", oper.StandardOrMacroOperator));
                }
            }
        }

        private readonly IOperatorRepository _operatorRepository;
        private IOperatorRepository OperatorRepository
        {
            get { return _operatorRepository; }
        }

        public ObservableCollectionEx<OperatorVm> StandardOperatorVms
        {
            get { return _standardOperatorVms; }
            set { _standardOperatorVms = value; }
        }

        public ObservableCollectionEx<OperatorVm> MacroOperatorVms
        {
            get { return _macroOperatorVms; }
            set { _macroOperatorVms = value; }
        }
    }
}
