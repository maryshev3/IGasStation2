using IGasStation2.Commands;
using IGasStation2.EntityFrameworkContexts;
using IGasStation2.Models;
using IGasStation2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IGasStation2.ViewModels
{
    public class AnalyzeVM : ViewModelBase
    {
        private readonly GasStationContext _gasStationContext;
        private readonly NormalizeChecker _normalizeChecker;

        private string _yearText;
        private string _selectedAnalyzeType = Constants.AllowedPower;
        private string _setNormallyType = Constants.SetIsNotChecked;
        private string _deleteHelpType = Constants.DeleteToNormallyWillNotHelp;
        private List<GasStation> _badDataGasStation;
        private List<GasStationPowerUsing> _badDataGasStationPowerUsing;

        public string[] AnalyzeTypes { get; } =
        {
            Constants.AllowedPower,
            Constants.CurrentPower,
            Constants.PowerUsagePerYear
        };

        public string SetNormallyType 
        {
            get => _setNormallyType;
            private set 
            {
                SetProperty(ref _setNormallyType, value);
                OnPropertyChanged(nameof(SetNormallyFieldVisibility));
                OnPropertyChanged(nameof(DeleteToNormallyFieldVisibility));
                OnPropertyChanged(nameof(BadDataGasStationVisibility));
                OnPropertyChanged(nameof(BadDataPowerUsingsVisibility));
            }
        }

        public string DeleteHelpType
        {
            get => _deleteHelpType;
            private set
            {
                SetProperty(ref _deleteHelpType, value);
                OnPropertyChanged(nameof(DeleteToNormallyFieldVisibility));
            }
        }

        public List<GasStation> BadDataGasStation
        {
            get => _badDataGasStation;
            set
            {
                SetProperty(ref _badDataGasStation, value);
            }
        }

        public List<GasStationPowerUsing> BadDataGasStationPowerUsing
        {
            get => _badDataGasStationPowerUsing;
            set
            {
                SetProperty(ref _badDataGasStationPowerUsing, value);
            }
        }

        public Visibility SetNormallyFieldVisibility 
        {
            get => SetNormallyType != Constants.SetIsNotChecked && SetNormallyType != Constants.SetIsNormally && SetNormallyType != Constants.SetIsNotNormallyNeedAppend
                ? Visibility.Visible 
                : Visibility.Collapsed;
        }

        public Visibility DeleteToNormallyFieldVisibility
        {
            get => SetNormallyType != Constants.SetIsNotChecked && DeleteHelpType == Constants.DeleteToNormallyWillHelp
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public Visibility BadDataGasStationVisibility
        {
            get => SetNormallyType == Constants.SetIsNotNormally && SelectedAnalyzeType != Constants.PowerUsagePerYear
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public Visibility BadDataPowerUsingsVisibility
        {
            get => SetNormallyType == Constants.SetIsNotNormally && SelectedAnalyzeType == Constants.PowerUsagePerYear
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public string YearText
        {
            get => _yearText;
            set => SetProperty(ref _yearText, value);
        }

        public string SelectedAnalyzeType
        {
            get => _selectedAnalyzeType;
            set
            {
                SetProperty(ref _selectedAnalyzeType, value);
                OnPropertyChanged(nameof(VisibilityYear));
            }
        }

        public Visibility VisibilityYear
        {
            get => SelectedAnalyzeType == Constants.PowerUsagePerYear
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public AnalyzeVM(GasStationContext gasStationContext, NormalizeChecker normalizeChecker) 
        {
            _gasStationContext = gasStationContext;
            _normalizeChecker = normalizeChecker;

            CheckNormallyClick = new AsyncCommand(OnCheckNormallyClick);
        }

        private Task OnCheckNormallyClick(object param) 
        {
            //Тут проверка на нормированность
            if (SelectedAnalyzeType == Constants.PowerUsagePerYear && String.IsNullOrWhiteSpace(YearText))
            {
                MessageBox.Show("Не введено значение для года, по которому проводится анализ");
                return Task.CompletedTask;
            }

            //Список значений, по которым проверяется нормированность (либо потребление за год, либо разрешенная мощность, либо установленная мощность)
            List<int> items = default;
            int year = default;

            if (SelectedAnalyzeType == Constants.PowerUsagePerYear)
            {
                Boolean isNumeric = int.TryParse(YearText, out year);

                if (!isNumeric)
                {
                    MessageBox.Show("Введённый год не является числом");
                    return Task.CompletedTask;
                }

                items = _gasStationContext.GasStationPowerUsings.Where(x => x.Year == year).Select(x => x.PowerUsing).ToList();
            }
            if (SelectedAnalyzeType == Constants.CurrentPower)
            {
                items = _gasStationContext.GasStations.Select(x => x.CurrentPower).ToList();
            }
            if (SelectedAnalyzeType == Constants.AllowedPower)
            {
                items = _gasStationContext.GasStations.Select(x => x.AllowedPower).ToList();
            }

            if (_normalizeChecker.IsDataNormalized(items)) 
            {
                SetNormallyType = Constants.SetIsNormally;

                return Task.CompletedTask;
            }

            var response = _normalizeChecker.BadData(items);

            if (!response.badData.Any())
            {
                SetNormallyType = Constants.SetIsNotNormallyNeedAppend;

                return Task.CompletedTask;
            }

            SetNormallyType = Constants.SetIsNotNormally;

            if (SelectedAnalyzeType == Constants.PowerUsagePerYear)
            {
                BadDataGasStationPowerUsing = _gasStationContext
                    .GasStationPowerUsings
                    .Where(x => x.Year == year && response.badData.Contains(x.PowerUsing))
                    .ToList();
            }

            if (SelectedAnalyzeType == Constants.CurrentPower)
            {
                BadDataGasStation = _gasStationContext
                    .GasStations
                    .Where(x => response.badData.Contains(x.CurrentPower))
                    .ToList();
            }

            if (SelectedAnalyzeType == Constants.AllowedPower)
            {
                BadDataGasStation = _gasStationContext
                    .GasStations
                    .Where(x => response.badData.Contains(x.AllowedPower))
                    .ToList();
            }

            return Task.CompletedTask;
        }

        public ICommand CheckNormallyClick { get; }
    }
}
