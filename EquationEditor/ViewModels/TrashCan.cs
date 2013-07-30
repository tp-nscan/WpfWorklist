using System.Windows;
using EquationEditor.ViewModels.Equation;
using EquationEditor.ViewModels.Operator;
using WpfUtils;
using WpfUtils.DragAndDrop;

namespace EquationEditor.ViewModels
{
    public class TrashCan : IDropTarget
    {
        public void DragOver(DropInfo dropInfo)
        {
            if( (dropInfo==null) || (dropInfo.DragInfo == null))
            {
                return;
            }
            var sourceNode = dropInfo.DragInfo.Data as EquationNodeBaseVm;
            if (sourceNode == null)
            {
                return;
            }
            dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            dropInfo.Effects = DragDropEffects.All;
        }

        public void Drop(DropInfo dropInfo)
        {
            var sourceNode = dropInfo.DragInfo.Data as EquationNodeBaseVm;
            if (sourceNode == null)
            {
                return;
            }

            if (sourceNode.EquationNodeVmType != EquationNodeVmType.Root)
            {
                var arg = new EquationOperatorArgNodeVm(sourceNode.ParentNode.AllowedScampsDataType.ToResourceName(),
                    sourceNode.ParentNode.AllowedScampsDataType) { IsNodeExpanded = true, };

                sourceNode.ParentNode.ChildNodeVms.Replace(sourceNode, arg);
                arg.ParentNode = sourceNode.ParentNode;
            }

        }
    }
}
