using IGasStation2.Commands;
using IGasStation2.EntityFrameworkContexts;
using IGasStation2.Models;
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
        private readonly GasStationContext _gasStationContext;

        private string _nameForSearch;
        private string _locationForSearch;
        private string _phoneNumberForSearch;
        private string _emailForSearch;
        private string _allowedPowerForSearch;
        private string _currentPowerForSearch;
        private string _powerDiselGeneratorForSearch;
        private string _typeAndPowerForSearch;
        private IEnumerable<GasStation> _gasStations;

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
        public IEnumerable<GasStation> GasStations
        {
            get => _gasStations;
            set => SetProperty(ref _gasStations, value);
        }

        public ShowDbVM(GasStationContext gasStationContext) 
        {
            _gasStationContext = gasStationContext;

            SearchClick = new AsyncCommand(OnSearchClick);
            ClearClick = new AsyncCommand(OnClearClick);

            GasStations = _gasStationContext.GasStations.ToList();

            ;
        }

        private Task OnSearchClick(object param)
        {
            MessageBox.Show(NameForSearch);
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
    }
}
