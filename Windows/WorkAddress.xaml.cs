using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Windows;

namespace Pro_Practice.Windows
{
    /// <summary>
    /// Логика взаимодействия для WorkAddress.xaml
    /// </summary>
    public partial class WorkAddress : Window
    {
        private BochagovaProPracticeContext dbContext;
        private Address _address;
        private AddressVM _addressVM;

        public WorkAddress()
        {
            InitializeComponent();
            dbContext = new BochagovaProPracticeContext(new DbContextOptions<BochagovaProPracticeContext>());
            
            LoadComboBoxes();
        }

        public WorkAddress(AddressVM addressVM) : this()
        {
            _addressVM = addressVM;
            _address = dbContext.Addresses.FirstOrDefault(w => w.IdAddress == _addressVM.IdAddress);

            if (_address != null)
            {
                latitudeTextBox.Text = _address.Latitude;
                longitudeTextBox.Text = _address.Longitude;
                regionTextBox.Text = _address.Region;
                districtTextBox.Text = _address.District;
                cityTextBox.Text = _address.City;
                localityTextBox.Text = _address.Locality;
                streetTextBox.Text = _address.Street;
                houseOrLandPlotTextBox.Text = _address.HouseOrLandPlot;
                roomTextBox.Text = _address.Room;

                comboBoxDivision.SelectedItem = dbContext.Divisions
                    .FirstOrDefault(d => d.IdDivision == _address.IdDivision);
                comboBoxCounterparty.SelectedItem = dbContext.Counterparties
                    .FirstOrDefault(s => s.IdCounterpartie == _address.IdCounterpartie);
                comboBoxWarehouse.SelectedItem = dbContext.Warehouses
                    .FirstOrDefault(s => s.IdWarehouse == _address.IdWarehouse);
            }
        }

        private void LoadComboBoxes()
        {
            if (App.currentUser.IdRole == 1)
            {
                comboBoxCounterparty.Visibility = Visibility.Collapsed;
                CounterpartieText.Visibility = Visibility.Collapsed;

                comboBoxDivision.Margin = new Thickness(0, 40, 30, 0);
                comboBoxWarehouse.Margin = new Thickness(0, 110, 30, 0);
                latitudeTextBox.Margin = new Thickness(0, 180, 30, 0);
                longitudeTextBox.Margin = new Thickness(0, 250, 30, 0);
                regionTextBox.Margin = new Thickness(0, 320, 30, 0);
                districtTextBox.Margin = new Thickness(0, 390, 30, 0);
                cityTextBox.Margin = new Thickness(0, 460, 30, 0);
                localityTextBox.Margin = new Thickness(0, 530, 30, 0);
                streetTextBox.Margin = new Thickness(0, 600, 30, 0);
                houseOrLandPlotTextBox.Margin = new Thickness(0, 670, 30, 0);
                roomTextBox.Margin = new Thickness(0, 740, 30, 0);

                DivisionText.Margin = new Thickness(0, 40, 450, 0);
                WarehouseText.Margin = new Thickness(0, 110, 450, 0);
                LatitudeText.Margin = new Thickness(0, 180, 450, 0);
                LongtitudeText.Margin = new Thickness(0, 250, 450, 0);
                RegionText.Margin = new Thickness(0, 320, 450, 0);
                DistrictText.Margin = new Thickness(0, 390, 450, 0);
                CityText.Margin = new Thickness(0, 460, 450, 0);
                LocalityText.Margin = new Thickness(0, 530, 450, 0);
                StreetText.Margin = new Thickness(0, 600, 450, 0);
                HousePlotText.Margin = new Thickness(0, 670, 450, 0);
                RoomText.Margin = new Thickness(0, 740, 450, 0);

                Height = 940;
            }

            if (App.currentUser.IdRole == 2)
            {
                comboBoxDivision.Visibility = Visibility.Collapsed;
                comboBoxWarehouse.Visibility = Visibility.Collapsed;
                DivisionText.Visibility = Visibility.Collapsed;
                WarehouseText.Visibility = Visibility.Collapsed;

                latitudeTextBox.Margin = new Thickness(0, 140, 30, 0);
                longitudeTextBox.Margin = new Thickness(0, 230, 30, 0);
                regionTextBox.Margin = new Thickness(0, 310, 30, 0);
                districtTextBox.Margin = new Thickness(0, 390, 30, 0);
                cityTextBox.Margin = new Thickness(0, 470, 30, 0);
                localityTextBox.Margin = new Thickness(0, 550, 30, 0);
                streetTextBox.Margin = new Thickness(0, 630, 30, 0);
                houseOrLandPlotTextBox.Margin = new Thickness(0, 710, 30, 0);
                roomTextBox.Margin = new Thickness(0, 790, 30, 0);

                LatitudeText.Margin = new Thickness(0, 140, 453, 0);
                LongtitudeText.Margin = new Thickness(0, 230, 453, 0);
                RegionText.Margin = new Thickness(0, 310, 453, 0);
                DistrictText.Margin = new Thickness(0, 390, 453, 0);
                CityText.Margin = new Thickness(0, 470, 453, 0);
                LocalityText.Margin = new Thickness(0, 550, 453, 0);
                StreetText.Margin = new Thickness(0, 630, 453, 0);
                HousePlotText.Margin = new Thickness(0, 710, 453, 0);
                RoomText.Margin = new Thickness(0, 790, 453, 0);

            }

            comboBoxCounterparty.ItemsSource = dbContext.Counterparties.ToList();
            comboBoxCounterparty.DisplayMemberPath = "NameCp"; 

            comboBoxDivision.ItemsSource = dbContext.Divisions.ToList();
            comboBoxDivision.DisplayMemberPath = "NameDivision"; 

            comboBoxWarehouse.ItemsSource = dbContext.Warehouses.ToList();
            comboBoxWarehouse.DisplayMemberPath = "NameWarehouse"; 
        }

        private bool AreRequiredFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(latitudeTextBox.Text))
            {
                MessageBox.Show("Поле 'Широта' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(longitudeTextBox.Text))
            {
                MessageBox.Show("Поле 'Долгота' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(regionTextBox.Text))
            {
                MessageBox.Show("Поле 'Регион' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(districtTextBox.Text))
            {
                MessageBox.Show("Поле 'Район' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(cityTextBox.Text))
            {
                MessageBox.Show("Поле 'Город' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(localityTextBox.Text))
            {
                MessageBox.Show("Поле 'Населенный пункт' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(streetTextBox.Text))
            {
                MessageBox.Show("Поле 'Улица' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(houseOrLandPlotTextBox.Text))
            {
                MessageBox.Show("Поле 'Дом/Участок' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(roomTextBox.Text))
            {
                MessageBox.Show("Поле 'Помещение' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!AreRequiredFieldsFilled())
            {
                return;
            }
            string latitude = latitudeTextBox.Text;
            string longitude = longitudeTextBox.Text;
            string region = regionTextBox.Text;
            string district = districtTextBox.Text;
            string city = cityTextBox.Text;
            string locality = localityTextBox.Text;
            string street = streetTextBox.Text;
            string houseOrLandPlot = houseOrLandPlotTextBox.Text;
            string room = roomTextBox.Text;

            int? selectedCounterpartyId = (comboBoxCounterparty.SelectedItem as Counterparty)?.IdCounterpartie;

            int? selectedDivisionId = (comboBoxDivision.SelectedItem as Models.Division)?.IdDivision;

            int? selectedWarehouseId = (comboBoxWarehouse.SelectedItem as Models.Warehouse)?.IdWarehouse;

            if (_address == null)  
            {
                var newAddress = new Address
                {
                    Latitude = latitude,
                    Longitude = longitude,
                    Region = region,
                    District = district,
                    City = city,
                    Locality = locality,
                    Street = street,
                    HouseOrLandPlot = houseOrLandPlot,
                    Room = room,
                    IdCounterpartie = selectedCounterpartyId,
                    IdDivision = selectedDivisionId,
                    IdWarehouse = selectedWarehouseId
                };

                dbContext.Add(newAddress); 
            }
            else 
            {
                _address.Latitude = latitude;
                _address.Longitude = longitude;
                _address.Region = region;
                _address.District = district;
                _address.City = city;
                _address.Locality = locality;
                _address.Street = street;
                _address.HouseOrLandPlot = houseOrLandPlot;
                _address.Room = room;
                _address.IdCounterpartie = selectedCounterpartyId;
                _address.IdDivision = selectedDivisionId;
                _address.IdWarehouse = selectedWarehouseId;

                dbContext.Update(_address); 
            }

            dbContext.SaveChanges(); 
            DialogResult = true; 
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();  
        }
    }
}