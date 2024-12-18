using Microsoft.EntityFrameworkCore;
using Pro_Practice.DivisionWindow;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Pro_Practice.DivisionPages
{
    public partial class Warehouse : Page
    {
        private BochagovaProPracticeContext _context = new BochagovaProPracticeContext();

        public ObservableCollection<WarehouseVM> ListWarehouses { get; set; }

        public Warehouse()
        {
            InitializeComponent();
            DataContext = this;
            ListWarehouses = new ObservableCollection<WarehouseVM>();
            LoadData();
            if (App.currentUser.IdRole != 1)
            {
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadData()
        {
            ListWarehouses.Clear();
            using (var context = new BochagovaProPracticeContext())
            {
                var warehouses = context.Warehouses
                    .Include(w => w.IdDivisionNavigation)
                    .Include(w => w.DefaultCellNavigation)
                    .Include(w => w.Addresses)
                    .ToList()
                    .Select(w => new WarehouseVM(w))
                    .ToList();

                foreach (var warehouseVM in warehouses)
                {
                    ListWarehouses.Add(warehouseVM);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var workWindow = new WorkWarehouse();
            if (workWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            WarehouseVM selectedWarehouse = (WarehouseVM)ListViewWarehouses.SelectedItem;
            if (selectedWarehouse != null)
            {
                var workWindow = new WorkWarehouse(selectedWarehouse);
                if (workWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }
    }
}