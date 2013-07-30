using System;
using System.ComponentModel;
using System.Windows;
using SampleControls.ViewModel;

namespace SampleControls.View
{
    /// <summary>
    /// Interaction logic for SamplePlate.xaml
    /// </summary>
    public partial class SamplePlate
    {
        public SamplePlate()
        {
            InitializeComponent();
            SizeChanged += SamplePlateSizeChanged;
        }

        private bool _widthIsChanging;
        private bool _heightIsChanging;

        void SamplePlateSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_widthIsChanging)
            {
                _widthIsChanging = false;
                return;
            }

            if (_heightIsChanging)
            {
                _heightIsChanging = false;
                return;
            }

            if(SamplePlateVm==null)
            {
                return;
            }

            double aspect = (SamplePlateVm.ColCount + 1);
            aspect /= (SamplePlateVm.RowCount + 1);

            var widthChange = e.NewSize.Width - e.PreviousSize.Width;
            if (Math.Abs(widthChange - 0) > double.Epsilon)
            {
                Height = e.NewSize.Width / aspect;
                LayoutWells();
                _heightIsChanging = true;
            }

            //border.BorderThickness = new Thickness(10,10,10,10);
            border.CornerRadius = new CornerRadius(e.NewSize.Width/20);
        }

        void LayoutWells()
        {
            var wellSide = ActualWidth / (SamplePlateVm.ColCount + 1);
            foreach (var wellVm in SamplePlateVm.WellVms)
            {
                switch (wellVm.SamplePlatePart)
                {
                    case SamplePlatePart.ColumnHeader:
                        wellVm.Location = new Point
                        (
                            wellSide / 2 +  wellVm.Column * wellSide,
                            0
                        );
                        break;
                    case SamplePlatePart.RowHeader:
                        wellVm.Location = new Point
                        (
                            0,
                            wellSide / 2 +  wellVm.Row * wellSide
                        );
                        break;
                    case SamplePlatePart.Well:
                        wellVm.Location = new Point
                        (
                            wellSide / 2 + wellVm.Column * wellSide,
                            wellSide / 2 + wellVm.Row * wellSide
                        );
                        break;
                }

                wellVm.SideLength = wellSide;

                //var lbi = new ListBoxItem();
                //lbi.s = false;
            }
        }

        #region SamplePlateVm

        [Category("Custom Properties")]
        public SamplePlateVm SamplePlateVm
        {
            get { return (SamplePlateVm)GetValue(SamplePlateVmProperty); }
            set { SetValue(SamplePlateVmProperty, value); }
        }

        public static readonly DependencyProperty SamplePlateVmProperty =
            DependencyProperty.Register("SamplePlateVm", typeof(SamplePlateVm), typeof(SamplePlate),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnSamplePlateVmPropertyChanged));

        private static void OnSamplePlateVmPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var samplePlate = d as SamplePlate;
            if(samplePlate != null)
            {
                samplePlate.LayoutWells();
            }
        }

        #endregion

    }
}
