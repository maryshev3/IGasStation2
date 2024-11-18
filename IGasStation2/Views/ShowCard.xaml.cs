using IGasStation2.Models;
using IGasStation2.Utils;
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

namespace IGasStation2.Views
{
    /// <summary>
    /// Interaction logic for ShowCard.xaml
    /// </summary>
    public partial class ShowCard : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PowerUsingPredicter _powerUsingPredicter;
        private readonly ShowCardVM _showCardVM;

        public ShowCard()
        {
            _serviceProvider = ViewModelLocator._serviceProvider;

            _powerUsingPredicter = _serviceProvider.GetService(typeof(PowerUsingPredicter)) as PowerUsingPredicter;
            _showCardVM = _serviceProvider.GetService(typeof(ShowCardVM)) as ShowCardVM;

            InitializeComponent();
        }

        public void Init(List<GasStationPowerUsing> gasStationPowerUsings, int yearsPredictedCount = 0)
        {
            PowerUsingPlot.Plot.Clear();
            PowerUsingPlot.Plot.HideLegend();
            PowerUsingPlot.Refresh();

            DateTime[] a = gasStationPowerUsings.Select(x => new DateTime(x.Year, 12, 31)).ToArray();
            double[] b = gasStationPowerUsings.Select(x => (double)x.PowerUsing).ToArray();

            var originXYScatterOrigins = PowerUsingPlot.Plot.Add.Markers(a.Take(gasStationPowerUsings.Count - yearsPredictedCount).ToArray(), b.Take(gasStationPowerUsings.Count - yearsPredictedCount).ToArray());
            originXYScatterOrigins.Label = "Энергопотребления за года (исходные)";
            originXYScatterOrigins.MarkerSize = 6;

            var originXYScatterPredicted = PowerUsingPlot.Plot.Add.Markers(a.Skip(gasStationPowerUsings.Count - yearsPredictedCount).Take(yearsPredictedCount).ToArray(), b.Skip(gasStationPowerUsings.Count - yearsPredictedCount).Take(yearsPredictedCount).ToArray(), color: ScottPlot.Color.FromHex("008000"));
            originXYScatterPredicted.Label = "Энергопотребления за года (предсказанные)";
            originXYScatterPredicted.MarkerSize = 6;

            PowerUsingPlot.Plot.ShowLegend(Alignment.UpperRight);

            PowerUsingPlot.Plot.YLabel("Потребление электроэнергии, кВт", 14);

            PowerUsingPlot.Plot.Axes.DateTimeTicksBottom();

            PowerUsingPlot.Refresh();
        }

        private void PredictButton_Click(object sender, RoutedEventArgs e)
        {
            Init(_powerUsingPredicter.Predict(_showCardVM.GasStationPowerUsings, 3), 3);
        }
    }
}
