using Microsoft.EntityFrameworkCore;
using Pro_Practice.CounterpartieWindow;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Pro_Practice.CounterpartiePages
{
    public partial class Counterparties : Page
    {
        private BochagovaProPracticeContext _context;

        public Counterparties()
        {
            InitializeComponent();
            _context = new BochagovaProPracticeContext();
            LoadCounterparties();
            if (App.currentUser.IdRole != 2)
            {
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadCounterparties()
        {
            var counterparties = _context.Counterparties
                .Include(c => c.Addresses)
                .Include(c => c.IdTypeCpNavigation)
                .Select(c => new CounterpartieVM
                {
                    NameCp = c.NameCp,
                    TypeOfCounterparty = c.IdTypeCpNavigation.NameType,
                    PassportData = c.PassportData,
                    TaxpayerIdentificationNumber = c.TaxpayerIdentificationNumber,
                    Address = c.Addresses.Any()
                        ? $"{c.Addresses.FirstOrDefault().Region} {c.Addresses.FirstOrDefault().District} {c.Addresses.FirstOrDefault().City} {c.Addresses.FirstOrDefault().Locality} " +
                          $"{c.Addresses.FirstOrDefault().Street} {c.Addresses.FirstOrDefault().HouseOrLandPlot} {c.Addresses.FirstOrDefault().Room}"
                        : "Адрес не указан"
                })
                .ToList();

            CounterpartiesListView.ItemsSource = counterparties;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addCounterpartyWindow = new WorkCounterpartie();
            if (addCounterpartyWindow.ShowDialog() == true)
            {
                LoadCounterparties();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            CounterpartieVM selectedCounterparty = (CounterpartieVM)CounterpartiesListView.SelectedItem;
            if(selectedCounterparty != null)
            {
                var editCounterpartyWindow = new WorkCounterpartie(selectedCounterparty);
                if (editCounterpartyWindow.ShowDialog() == true)
                {
                    LoadCounterparties();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите контрагента для редактирования.");
            }
        }
    }
}