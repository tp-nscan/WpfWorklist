using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SorterControls.ViewModels.Entities;
using WpfUtils.DataGridCollection;

namespace SorterControls.Views.Entities
{
    /// <summary>
    /// Interaction logic for SorterResultPoolControl.xaml
    /// </summary>
    public partial class SorterResultPoolControl : UserControl
    {
        public SorterResultPoolControl()
        {
            InitializeComponent();
        }

        #region SorterResultPool

        public static readonly DependencyProperty SorterResultPoolVmProperty =
            DependencyProperty.Register("SorterResultPoolVm", typeof(ISorterResultPoolVm), typeof(SorterResultPoolControl),
                new FrameworkPropertyMetadata(OnSorterResultPoolChanged));

        public ISorterResultPoolVm SorterResultPoolVm
        {
            get { return (ISorterResultPoolVm)GetValue(SorterResultPoolVmProperty); }
            set { SetValue(SorterResultPoolVmProperty, value); }
        }

        private static void OnSorterResultPoolChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sorterResultPoolVm = e.NewValue as ISorterResultPoolVm;
            var sorterResultPoolControl = d as SorterResultPoolControl;

            if ((sorterResultPoolVm == null) || (sorterResultPoolControl == null))
            {
                return;
            }

            var colList = sorterResultPoolControl.MyDataGrid.Columns.ToList();
            foreach (var column in colList)
            {
                if (((string) column.Header).Contains("Sw_"))
                {
                    sorterResultPoolControl.MyDataGrid.Columns.Remove(column);
                }
            }

            for (var i = 0; i < sorterResultPoolVm.SwitchesPerSorterResult ; i++)
            {
                if (! sorterResultPoolVm.SorterResultVms.Any(T => T.SwitchResultVms[i].UseCount > 0))
                {
                    continue;
                }

                var propertyPathName = "SwitchResultVms[" + i + "]";
                var bind = new Binding {Path = new PropertyPath(propertyPathName), Mode = BindingMode.OneWay};

                var cbc = new CustomBoundColumn
                    {
                        Header = "Sw_" + i,
                        Binding = bind,
                        TemplateName = "CustomTemplate",
                        SortMemberPath = propertyPathName + ".UseFraction" 
                    };

                sorterResultPoolControl.MyDataGrid.Columns.Add(cbc);
            }
        }

        //var col = new DataGridTemplateColumn();
        //col.Header = "Sw_" + i;
        //var propertyPathName = "Item[" + i + "]";
        //col.SortMemberPath = propertyPathName;
        ////col.Binding = new Binding(path:"Item["+i+"]");
        //dataGridCollectionControl.MyDataGrid.Columns.Add(col);

        //// First: create and add the data template to the parent control
        //var dt = new DataTemplate(typeof(TextBox));
        //col.CellTemplate = dt;

        //// Second: create and add the text box to the data template
        //var txtElement = new FrameworkElementFactory(typeof(TextBox));
        //dt.VisualTree = txtElement;

        //// Create binding
        //var bind = new Binding();
        //bind.Path = new PropertyPath(propertyPathName);
        //bind.Mode = BindingMode.TwoWay;

        //// Third: set the binding in the text box
        //txtElement.SetBinding(TextBox.TextProperty, bind);
        //txtElement.SetValue(FontSizeProperty, 18.0);
        #endregion

        class CustomBoundColumn : DataGridBoundColumn
        {
            public string TemplateName { get; set; }

            protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
            {
                var binding = new Binding(((Binding)Binding).Path.Path)
                {
                    Source = dataItem,
                    Mode = BindingMode.OneWay
                };

                var content = new ContentControl();
                content.ContentTemplate = (DataTemplate)cell.FindResource(TemplateName);
                content.SetBinding(ContentProperty, binding);
                return content;
            }

            protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
            {
                return GenerateElement(cell, dataItem);
            }
        }
    }
}
