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

        public ShowCardVM(GasStationUtil gasStationUtil) 
        {
            _gasStationUtil = gasStationUtil;

            EditClick = new AsyncCommand(OnEditClick);
        }

        private Task OnEditClick(object param)
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

        public ICommand EditClick { get; }
    }
}
