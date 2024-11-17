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
    public class ShowDbVM : ViewModelBase
    {
        private readonly GasStationUtil _gasStationUtil;

        private string _nameForSearch = String.Empty;
        private string _locationForSearch = String.Empty;
        private string _phoneNumberForSearch = String.Empty;
        private string _emailForSearch = String.Empty;
        private string _allowedPowerForSearch = String.Empty;
        private string _currentPowerForSearch = String.Empty;
        private string _powerDiselGeneratorForSearch = String.Empty;
        private string _typeAndPowerForSearch = String.Empty;

        private List<GasStation> _gasStations;
        private GasStation _selectedGasStation;

        public string NameForSearch 
        {
            get => _nameForSearch;
            set => SetProperty(ref _nameForSearch, value);
        }
        public string LocationForSearch
        {
            get => _locationForSearch;
            set => SetProperty(ref _locationForSearch, value);
        }
        public string PhoneNumberForSearch
        {
            get => _phoneNumberForSearch;
            set => SetProperty(ref _phoneNumberForSearch, value);
        }
        public string EmailForSearch
        {
            get => _emailForSearch;
            set => SetProperty(ref _emailForSearch, value);
        }
        public string AllowedPowerForSearch
        {
            get => _allowedPowerForSearch;
            set => SetProperty(ref _allowedPowerForSearch, value);
        }
        public string CurrentPowerForSearch
        {
            get => _currentPowerForSearch;
            set => SetProperty(ref _currentPowerForSearch, value);
        }
        public string PowerDiselGeneratorForSearch
        {
            get => _powerDiselGeneratorForSearch;
            set => SetProperty(ref _powerDiselGeneratorForSearch, value);
        }
        public string TypeAndPowerForSearch
        {
            get => _typeAndPowerForSearch;
            set => SetProperty(ref _typeAndPowerForSearch, value);
        }

        public List<GasStation> GasStations
        {
            get => _gasStations;
            set => SetProperty(ref _gasStations, value);
        }
        public GasStation SelectedGasStation
        {
            get => _selectedGasStation;
            set 
            { 
                SetProperty(ref _selectedGasStation, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public bool IsEnabled => SelectedGasStation != null;

        public ShowDbVM(GasStationUtil gasStationUtil) 
        {
            _gasStationUtil = gasStationUtil;

            SearchClick = new AsyncCommand(OnSearchClick);
            ClearClick = new AsyncCommand(OnClearClick);
            RemoveClick = new AsyncCommand(OnRemoveClick);

            GasStations = _gasStationUtil.GetGasStations(
                NameForSearch,
                LocationForSearch,
                PhoneNumberForSearch,
                EmailForSearch,
                AllowedPowerForSearch,
                CurrentPowerForSearch,
                PowerDiselGeneratorForSearch,
                TypeAndPowerForSearch
            );
        }

        private Task OnSearchClick(object param)
        {
            GasStations = _gasStationUtil.GetGasStations(
                NameForSearch,
                LocationForSearch,
                PhoneNumberForSearch,
                EmailForSearch,
                AllowedPowerForSearch,
                CurrentPowerForSearch,
                PowerDiselGeneratorForSearch,
                TypeAndPowerForSearch
            );

            return Task.CompletedTask;
        }

        public ICommand SearchClick { get; }

        private Task OnClearClick(object param)
        {
            NameForSearch
                = LocationForSearch
                = PhoneNumberForSearch
                = EmailForSearch
                = AllowedPowerForSearch
                = CurrentPowerForSearch
                = PowerDiselGeneratorForSearch
                = TypeAndPowerForSearch
                = String.Empty;

            return Task.CompletedTask;
        }

        public ICommand ClearClick { get; }

        private Task OnRemoveClick(object param)
        {
            _gasStationUtil.RemoveGasStation(_selectedGasStation);

            GasStations.Remove(_selectedGasStation);
            GasStations = GasStations;

            return Task.CompletedTask;
        }

        public ICommand RemoveClick { get; }
    }
}
