using IGasStation2.Models;
using IGasStation2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using IGasStation2.Commands;

namespace IGasStation2.ViewModels
{
    public class AddToDbVM : ViewModelBase
    {
        private readonly GasStationUtil _gasStationUtil;

        private string _nameForInsert = String.Empty;
        private string _locationForInsert = String.Empty;
        private string _coordinatesForInsert = String.Empty;
        private string _phoneNumberForInsert = String.Empty;
        private string _emailForInsert = String.Empty;
        private string _allowedPowerForInsert = String.Empty;
        private string _currentPowerForInsert = String.Empty;
        private string _powerDiselGeneratorForInsert = String.Empty;
        private string _typeAndPowerForInsert = String.Empty;
        private string _noteForInsert = String.Empty;

        public string NameForInsert
        {
            get => _nameForInsert;
            set => SetProperty(ref _nameForInsert, value);
        }
        public string LocationForInsert
        {
            get => _locationForInsert;
            set => SetProperty(ref _locationForInsert, value);
        }
        public string CoordinatesForInsert
        {
            get => _coordinatesForInsert;
            set => SetProperty(ref _coordinatesForInsert, value);
        }
        public string PhoneNumberForInsert
        {
            get => _phoneNumberForInsert;
            set => SetProperty(ref _phoneNumberForInsert, value);
        }
        public string EmailForInsert
        {
            get => _emailForInsert;
            set => SetProperty(ref _emailForInsert, value);
        }
        public string AllowedPowerForInsert
        {
            get => _allowedPowerForInsert;
            set => SetProperty(ref _allowedPowerForInsert, value);
        }
        public string CurrentPowerForInsert
        {
            get => _currentPowerForInsert;
            set => SetProperty(ref _currentPowerForInsert, value);
        }
        public string PowerDiselGeneratorForInsert
        {
            get => _powerDiselGeneratorForInsert;
            set => SetProperty(ref _powerDiselGeneratorForInsert, value);
        }
        public string TypeAndPowerForInsert
        {
            get => _typeAndPowerForInsert;
            set => SetProperty(ref _typeAndPowerForInsert, value);
        }
        public string NoteForInsert
        {
            get => _noteForInsert;
            set => SetProperty(ref _noteForInsert, value);
        }

        public AddToDbVM(GasStationUtil gasStationUtil) 
        {
            _gasStationUtil = gasStationUtil;

            InsertGasStationClick = new ActionCommand(OnInsertGasStationClick);
        }

        private void ClearInsertData()
        {
            NameForInsert
                = LocationForInsert
                = CoordinatesForInsert
                = PhoneNumberForInsert
                = EmailForInsert
                = AllowedPowerForInsert
                = CurrentPowerForInsert
                = PowerDiselGeneratorForInsert
                = TypeAndPowerForInsert
                = NoteForInsert
                = String.Empty;
        }

        private void OnInsertGasStationClick(object param)
        {
            try
            {
                _gasStationUtil.InsertGasStation(
                    NameForInsert,
                    LocationForInsert,
                    CoordinatesForInsert,
                    PhoneNumberForInsert,
                    EmailForInsert,
                    AllowedPowerForInsert,
                    CurrentPowerForInsert,
                    PowerDiselGeneratorForInsert,
                    TypeAndPowerForInsert,
                    NoteForInsert
                );

                ClearInsertData();

                MessageBox.Show("Запись была успешно добавлена");
            }
            catch
            {
                MessageBox.Show("Вы допустили ошибку при заполнении числовых данных");
            }
        }

        public ICommand InsertGasStationClick { get; }
    }
}
