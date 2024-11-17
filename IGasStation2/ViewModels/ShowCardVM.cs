using IGasStation2.Commands;
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
    public class ShowCardVM : ViewModelBase
    {
        private readonly GasStationUtil _gasStationUtil;

        private GasStation _gasStation;
        private List<GasStationPowerUsing> _gasStationPowerUsings;
        private GasStationPowerUsing _selectedGasStationPowerUsing;

        private string _nameForEdit = String.Empty;
        private string _locationForEdit = String.Empty;
        private string _coordinatesForEdit = String.Empty;
        private string _phoneNumberForEdit = String.Empty;
        private string _emailForEdit = String.Empty;
        private string _allowedPowerForEdit = String.Empty;
        private string _currentPowerForEdit = String.Empty;
        private string _powerDiselGeneratorForEdit = String.Empty;
        private string _typeAndPowerForEdit = String.Empty;
        private string _noteForEdit = String.Empty;

        private string _yearForEdit = String.Empty;
        private string _powerUsingForEdit = String.Empty;

        public string NameForEdit
        {
            get => _nameForEdit;
            set => SetProperty(ref _nameForEdit, value);
        }
        public string LocationForEdit
        {
            get => _locationForEdit;
            set => SetProperty(ref _locationForEdit, value);
        }
        public string CoordinatesForEdit
        {
            get => _coordinatesForEdit;
            set => SetProperty(ref _coordinatesForEdit, value);
        }
        public string PhoneNumberForEdit
        {
            get => _phoneNumberForEdit;
            set => SetProperty(ref _phoneNumberForEdit, value);
        }
        public string EmailForEdit
        {
            get => _emailForEdit;
            set => SetProperty(ref _emailForEdit, value);
        }
        public string AllowedPowerForEdit
        {
            get => _allowedPowerForEdit;
            set => SetProperty(ref _allowedPowerForEdit, value);
        }
        public string CurrentPowerForEdit
        {
            get => _currentPowerForEdit;
            set => SetProperty(ref _currentPowerForEdit, value);
        }
        public string PowerDiselGeneratorForEdit
        {
            get => _powerDiselGeneratorForEdit;
            set => SetProperty(ref _powerDiselGeneratorForEdit, value);
        }
        public string TypeAndPowerForEdit
        {
            get => _typeAndPowerForEdit;
            set => SetProperty(ref _typeAndPowerForEdit, value);
        }
        public string NoteForEdit
        {
            get => _noteForEdit;
            set => SetProperty(ref _noteForEdit, value);
        }
        public string YearForEdit
        {
            get => _yearForEdit;
            set => SetProperty(ref _yearForEdit, value);
        }
        public string PowerUsingForEdit
        {
            get => _powerUsingForEdit;
            set => SetProperty(ref _powerUsingForEdit, value);
        }

        public GasStation GasStation 
        {
            get => _gasStation;
            set 
            {
                SetProperty(ref _gasStation, value);

                NameForEdit = value.CompanyName;
                LocationForEdit = value.Location;
                CoordinatesForEdit = value.Coordinates;
                PhoneNumberForEdit = value.PhoneNumber;
                EmailForEdit = value.Email;
                AllowedPowerForEdit = value.AllowedPower.ToString();
                CurrentPowerForEdit = value.CurrentPower.ToString();
                PowerDiselGeneratorForEdit = value.PowerDiselGenerator?.ToString() ?? String.Empty;
                TypeAndPowerForEdit = value.TypeAndPower;
                NoteForEdit = value.Note ?? String.Empty;
            }
        }

        public List<GasStationPowerUsing> GasStationPowerUsings
        {
            get => _gasStationPowerUsings;
            set
            {
                SetProperty(ref _gasStationPowerUsings, value);
            }
        }

        public GasStationPowerUsing SelectedGasStationPowerUsing
        {
            get => _selectedGasStationPowerUsing;
            set
            {
                SetProperty(ref _selectedGasStationPowerUsing, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public bool IsEnabled => SelectedGasStationPowerUsing != null;

        public ShowCardVM(GasStationUtil gasStationUtil) 
        {
            _gasStationUtil = gasStationUtil;

            EditGasStationClick = new AsyncCommand(OnEditGasStationClick);
            MergePowerUsingClick = new AsyncCommand(OnMergePowerUsingClick);
            RemovePowerUsingClick = new AsyncCommand(OnRemovePowerUsingClick);
        }

        private Task OnEditGasStationClick(object param)
        {
            try
            {
                _gasStationUtil.EditGasStation(
                    GasStation,
                    NameForEdit,
                    LocationForEdit,
                    CoordinatesForEdit,
                    PhoneNumberForEdit,
                    EmailForEdit,
                    AllowedPowerForEdit,
                    CurrentPowerForEdit,
                    PowerDiselGeneratorForEdit,
                    TypeAndPowerForEdit,
                    NoteForEdit
                );
            }
            catch
            {
                MessageBox.Show("Вы допустили ошибку при заполнении числовых данных");
            }

            return Task.CompletedTask;
        }

        public ICommand EditGasStationClick { get; }

        private Task OnMergePowerUsingClick(object param)
        {
            try
            {
                _gasStationUtil.MergePowerUsing(GasStation, YearForEdit, PowerUsingForEdit);

                GasStationPowerUsings = _gasStationUtil.GetPowerUsings(_gasStation);
            }
            catch
            {
                MessageBox.Show("Вы допустили ошибку при заполнении числовых данных");
            }

            return Task.CompletedTask;
        }

        public ICommand MergePowerUsingClick { get; }

        private Task OnRemovePowerUsingClick(object param)
        {
            _gasStationUtil.RemovePowerUsing(SelectedGasStationPowerUsing);

            GasStationPowerUsings = _gasStationUtil.GetPowerUsings(_gasStation);

            return Task.CompletedTask;
        }

        public ICommand RemovePowerUsingClick { get; }
    }
}
