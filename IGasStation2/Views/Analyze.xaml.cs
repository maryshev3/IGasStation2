using IGasStation2.ViewModels;
using ScottPlot;
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
using Colors = ScottPlot.Colors;

namespace IGasStation2.Views
{
    /// <summary>
    /// Interaction logic for Analyze.xaml
    /// </summary>
    public partial class Analyze : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AnalyzeVM _analyzeVM;

        public Analyze()
        {
            InitializeComponent();

            _serviceProvider = ViewModelLocator._serviceProvider;
            _analyzeVM = _serviceProvider.GetService(typeof(AnalyzeVM)) as AnalyzeVM;
        }

        private void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            NormallyPlot.Plot.Clear();
            NormallyPlot.Plot.HideLegend();
            NormallyPlot.Refresh();

            _analyzeVM.AnalyzeClick.Execute(null);

            BuildGraph(
                _analyzeVM.XYValuesCurve,
                _analyzeVM.XYValuesForApproximatelyCurve,
                _analyzeVM.XYAllowedPlusDeltaCurve,
                _analyzeVM.XYAllowedMinusDeltaCurve
            );
        }

        private void BuildGraph(Coordinates[] xyValuesCurve, Coordinates[] xyValuesForApproximatelyCurve, Coordinates[] xyAllowedPlusDeltaCurve, Coordinates[] xyAllowedMinusDeltaCurve)
        {
            //Строим графики

            var originXYScatter = NormallyPlot.Plot.Add.Markers(xyValuesCurve);
            originXYScatter.Label = "Ранговое распределение";
            originXYScatter.MarkerSize = 6;

            var approximatlyXYScatter = NormallyPlot.Plot.Add.Scatter(xyValuesForApproximatelyCurve);
            approximatlyXYScatter.Label = "Апроксимационное распределение";
            approximatlyXYScatter.Color = Colors.Brown;

            var xyAllowedPlusDeltaScatter = NormallyPlot.Plot.Add.Scatter(xyAllowedPlusDeltaCurve);
            xyAllowedPlusDeltaScatter.Label = "Доверительный интервал +";
            xyAllowedPlusDeltaScatter.Color = Colors.Green;

            var xyAllowedMinusDeltaScatter = NormallyPlot.Plot.Add.Scatter(xyAllowedMinusDeltaCurve);
            xyAllowedMinusDeltaScatter.Label = "Доверительный интервал -";
            xyAllowedMinusDeltaScatter.Color = Colors.Red;

            NormallyPlot.Plot.Axes.AutoScale();
            NormallyPlot.Plot.ShowLegend(Alignment.UpperRight);

            NormallyPlot.Plot.XLabel("Номер ранга", 20);
            NormallyPlot.Plot.YLabel("Потребление электроэнергии, кВт", 20);

            NormallyPlot.Refresh();
        }
    }
}
