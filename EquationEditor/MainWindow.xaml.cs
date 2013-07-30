using System.Windows;
using EquationEditor.DesignData;
using EquationEditor.Models.Equation;
using EquationEditor.Models.Operator;
using EquationEditor.ViewModels;

namespace EquationEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new EquationEditorVm(
                new EquationBase("the equation name", ScampsDataType.Any, "(-b + (b^2 - 4*a*c)^0.5) / 2a"), 
                new DesignElementRepository(), 
                new OperatorRepository()) {Title = "Equation Editor" };
        }
    }
}
