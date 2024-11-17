using IGasStation2.Utils;
using IGasStation2.ViewModels;
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
    /// Interaction logic for ShowDb.xaml
    /// </summary>
    public partial class ShowDb : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ShowCard _showCard;
        private readonly ShowCardVM _showCardVM;
        private readonly ShowDbVM _showDbVM;
        private Window _showCardWindow;
        private readonly GasStationUtil _gasStationUtil;

        public ShowDb()
        {
            _serviceProvider = ViewModelLocator._serviceProvider;

            _showCardVM = _serviceProvider.GetService(typeof(ShowCardVM)) as ShowCardVM;
            _showDbVM = _serviceProvider.GetService(typeof(ShowDbVM)) as ShowDbVM;

            _showCard = new ShowCard();

            _gasStationUtil = _serviceProvider.GetService(typeof(GasStationUtil)) as GasStationUtil;

            InitializeComponent();
        }

        private void OpenSelectedGasStation_Click(object sender, RoutedEventArgs e)
        {
            _showCardWindow?.Close();

            _showCardVM.GasStation = _showDbVM.SelectedGasStation;
            _showCardVM.GasStationPowerUsings = _gasStationUtil.GetPowerUsings(_showDbVM.SelectedGasStation);

            _showCardWindow = new Window
            {
                //Icon = _icon,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize,
                Title = "Просмотр записи",
                Content = _showCard,
                DataContext = _showCardVM
            };

            _showCardWindow.ResizeMode = ResizeMode.CanResize;

            _showCardWindow.Show();
        }
    }
}
