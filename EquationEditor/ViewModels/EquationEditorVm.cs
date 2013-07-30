using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using EquationEditor.Models.Elements;
using EquationEditor.Models.Equation;
using EquationEditor.Models.Operator;
using EquationEditor.ViewModels.Elements;
using EquationEditor.ViewModels.Equation;
using EquationEditor.ViewModels.Operator;
using Microsoft.Practices.Prism.Commands;
using WpfUtils;
using WpfUtils.DragAndDrop;

namespace EquationEditor.ViewModels
{
    public class EquationEditorVm : ViewModelBase, IDropTarget
    {
        public EquationEditorVm(EquationBase equation, IElementRepository elementRepository, 
                                IOperatorRepository operatorRepository)
        {
            EquationVm = new EquationVm(equation);
            ElementListVm = new ElementListVm(elementRepository);
            OperatorsVm = new OperatorsVm(operatorRepository);
            IsAllExpanded = true;
        }

        private EquationVm _equationVm;
        public EquationVm EquationVm
        {
            get { return _equationVm; }
            set
            {
                _equationVm = value;
                OnPropertyChanged("EquationVm");
            }
        }

        private ElementListVm _elementListVm;
        public ElementListVm ElementListVm
        {
            get { return _elementListVm; }
            set { _elementListVm = value; }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public OperatorsVm OperatorsVm { get; set; }

        private ObservableCollection<EquationElementNodeVm> _eraseMe = new ObservableCollection<EquationElementNodeVm>();
        public ObservableCollection<EquationElementNodeVm> EraseMe
        {
            get { return _eraseMe; }
            set { _eraseMe = value; }
        }

        void IDropTarget.DragOver(DropInfo dropInfo)
        {
            var targetNode = dropInfo.TargetItem as EquationNodeBaseVm;
            var sourceNode = dropInfo.DragInfo.Data as IEquationPart;
            if ((targetNode == null) || (sourceNode == null))
            {
                dropInfo.Effects = DragDropEffects.None;
                return;
            }

            if (targetNode.CanAddChild(sourceNode))
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Copy;
                return;
            }

            //dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            dropInfo.Effects = DragDropEffects.None;

            //var d = dropInfo.Data as ElementVm;
            //if(d!= null)
            //{
            //    dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            //    dropInfo.Effects = DragDropEffects.Copy;
            //    return;
            //}

            //var d2 = dropInfo.Data as OperatorVm;
            //if (d2 != null)
            //{
            //    dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            //    dropInfo.Effects = DragDropEffects.Copy;
            //    return;
            //}
        }

        void IDropTarget.Drop(DropInfo dropInfo)
        {
            var targetNode = dropInfo.TargetItem as EquationNodeBaseVm;
            var sourceNode = dropInfo.DragInfo.Data as IEquationPart;
            if((targetNode==null) || (sourceNode==null))
            {
                return;
            }
            if(! targetNode.CanAddChild(sourceNode))
            {
                return;
            }

            var elementSource = sourceNode as ElementVm;
            if(elementSource != null)
            {
                AddAnElement(elementSource, targetNode);
                return;
            }

            var operatorSource = sourceNode as OperatorVm;
            if (operatorSource != null)
            {
                AddAnOperator(operatorSource, targetNode);
                return;
            }

            throw new Exception(string.Format("EquationNodeType {0} not handled", sourceNode.EquationNodeType));
        }

        void AddAnElement(ElementVm elementSource, EquationNodeBaseVm targetNode)
        {
            var elementNodeVm = new EquationElementNodeVm(3.0, elementSource.ElementDescr, elementSource.DisplayName,
                                                          "element", elementSource.ScampsDataType) { IsNodeExpanded = true };

            if (targetNode.EquationNodeVmType == EquationNodeVmType.Root)
            {
                elementNodeVm.ParentNode = targetNode;
                targetNode.ChildNodeVms.Add(elementNodeVm);
            }
            else // the drop target is a stub, so replace it with the new element
            {
                var parentNode = targetNode.ParentNode;
                elementNodeVm.ParentNode = parentNode;
                parentNode.ChildNodeVms.Replace(targetNode, elementNodeVm);
            }
        }

        void AddAnOperator(OperatorVm operatorSourceVm, EquationNodeBaseVm targetNode)
        {
            var operatorNodeVm = new EquationOperatorNodeVm(operatorSourceVm.Name, operatorSourceVm.Name,
                operatorSourceVm.ScampsDataType, operatorSourceVm.OperatorBase.Description) { IsNodeExpanded = true };
            foreach (var operatorArg in operatorSourceVm.OperatorBase.OperatorArgs)
            {
                var arg = new EquationOperatorArgNodeVm(
                    operatorArg.ScampsDataType.ToResourceName(), operatorArg.ScampsDataType) { IsNodeExpanded = true, };
                operatorNodeVm.ChildNodeVms.Add(arg);
                arg.ParentNode = operatorNodeVm;
            }

            if (targetNode.EquationNodeVmType == EquationNodeVmType.Root)
            {
                operatorNodeVm.ParentNode = targetNode;
                targetNode.ChildNodeVms.Add(operatorNodeVm);
            }
            else // the drop target is a stub, so replace it with the new operator
            {
                var parentNode = targetNode.ParentNode;
                operatorNodeVm.ParentNode = parentNode;
                parentNode.ChildNodeVms.Replace(targetNode, operatorNodeVm);
            }
        }

        private bool _isAllExpanded;
        public bool IsAllExpanded
        {
            get { return _isAllExpanded; }
            set
            {
                _isAllExpanded = value;
                foreach (var equationNodeBaseVm in EquationVm.ChildNodeVms)
                {
                    equationNodeBaseVm.IsAllExpanded = value;
                }
            }
        }

        private TrashCan _trashCan = new TrashCan();
        public TrashCan TrashCan
        {
            get { return _trashCan; }
            set { _trashCan = value; }
        }

        #region Save Command

        private ICommand _save;

        public ICommand Save
        {
            get
            {
                return _save ?? (_save = new DelegateCommand<string>(DoTheSave, arg => CanSave()));
            }
        }

        private bool CanSave()
        {
            return EquationVm.IsComplete;
        }

        void DoTheSave(string searchText)
        {

        }

        #endregion // Save Command
    }
}
