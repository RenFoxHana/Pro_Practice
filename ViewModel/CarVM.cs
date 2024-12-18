using Pro_Practice.Models;
using System.ComponentModel;

namespace Pro_Practice.ViewModel
{
    public class CarVM
    {
        private string _licensePlateNumber;
        private string _carBrand;

        public int IdCar { get; set; }

        public string NameCar => $"{CarBrand} {LicensePlateNumber}";

        public string LicensePlateNumber
        {
            get => _licensePlateNumber;
            set
            {
                if (_licensePlateNumber != value)
                {
                    _licensePlateNumber = value;
                    OnPropertyChanged(nameof(LicensePlateNumber));
                    OnPropertyChanged(nameof(NameCar));
                }
            }
        }

        public string CarBrand
        {
            get => _carBrand;
            set
            {
                if (_carBrand != value)
                {
                    _carBrand = value;
                    OnPropertyChanged(nameof(CarBrand));
                    OnPropertyChanged(nameof(NameCar));
                }
            }
        }

        public string Employee { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CarVM() { }

        public CarVM(Car car)
        {
            IdCar = car.IdCar;
            LicensePlateNumber = car.LicensePlateNumber;
            CarBrand = car.IdCarBrandNavigation.NameBrand;
            Employee = $"{car.IdEmployeeNavigation.LastName} {car.IdEmployeeNavigation.FirstName} {car.IdEmployeeNavigation.Patronymic}";
        }
    }
}
