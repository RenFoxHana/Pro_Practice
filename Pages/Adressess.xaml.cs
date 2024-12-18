using Microsoft.EntityFrameworkCore;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using Pro_Practice.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Pro_Practice.Pages
{
    /// <summary>
    /// Логика взаимодействия для Adressess.xaml
    /// </summary>
    public partial class Adressess : Page
    {
        private BochagovaProPracticeContext _context;
        public ObservableCollection<Address> Addresses { get; set; }
        public ObservableCollection<AddressVM> ListAddress { get; set; }
        public Adressess()
        {
            InitializeComponent();
            DataContext = this;
            ListAddress = new ObservableCollection<AddressVM>();
            LoadAddresses();
            if(App.currentUser.IdRole == 3)
            {
                EditButton.Visibility = Visibility.Collapsed;
                AddButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadAddresses()
        {
            ListAddress.Clear();
            using (var context = new BochagovaProPracticeContext())
            {
                var addresses = context.Addresses
            .Include(a => a.IdCounterpartieNavigation)
            .Include(a => a.IdDivisionNavigation)
            .Include(a => a.IdWarehouseNavigation)
            .ToList()
            .Select(a => new AddressVM(a))
            .ToList(); ;

                foreach (var address in addresses)
                {
                    ListAddress.Add(address);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WorkAddress workAddressWindow = new WorkAddress();
            bool? result = workAddressWindow.ShowDialog();
            if (result == true)
            {
                LoadAddresses();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAddress = (AddressVM)AddressListView.SelectedItem;
            if (selectedAddress != null)
            {
                WorkAddress workAddressWindow = new WorkAddress(selectedAddress);
                bool? result = workAddressWindow.ShowDialog();
                if (result == true)
                {
                    LoadAddresses();
                }
            }
        }

        private void AddressListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAddress = (AddressVM)AddressListView.SelectedItem;
            if (selectedAddress != null)
            {
                if ((selectedAddress.NameDivision != null || selectedAddress.NameWarehouse != null) && App.currentUser.IdRole == 2 && selectedAddress.NameCounterpatie == "Не указан")
                {
                    EditButton.Visibility = Visibility.Collapsed;
                }
                else if (selectedAddress.NameCounterpatie != null && App.currentUser.IdRole == 2)
                {
                    EditButton.Visibility = Visibility.Visible;
                }
                else if ((selectedAddress.NameDivision != null || selectedAddress.NameWarehouse != null) && App.currentUser.IdRole != 2 && selectedAddress.NameCounterpatie == "Не указан")
                {
                    EditButton.Visibility = Visibility.Visible;
                }
                else if (selectedAddress.NameCounterpatie != null && App.currentUser.IdRole != 2)
                {
                    EditButton.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}