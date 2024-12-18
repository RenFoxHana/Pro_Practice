using Microsoft.EntityFrameworkCore;
using Pro_Practice.DivisionWindow;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Pro_Practice.DivisionPages
{
    /// <summary>
    /// Логика взаимодействия для StorageCells.xaml
    /// </summary>
    public partial class StorageCells : Page
    {
        private BochagovaProPracticeContext _context = new BochagovaProPracticeContext();
        public ObservableCollection<StorageCellVM> ListStorageCell { get; set; }

        public StorageCells()
        {
            InitializeComponent();
            DataContext = this;
            ListStorageCell = new ObservableCollection<StorageCellVM>();
            LoadData();
            if (App.currentUser.IdRole != 1)
            {
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadData()
        {
            ListStorageCell.Clear();
            using (var context = new BochagovaProPracticeContext())
            {
                var cells = context.StorageCells
                    .Include(e => e.IdWarehouseNavigation)
                    .ToList()
                    .Select(e => new StorageCellVM(e))
                    .ToList();

                foreach (var cell in cells)
                {
                    ListStorageCell.Add(cell);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var workWindow = new WorkStorageCell();
            if (workWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            StorageCellVM selectedCelll = (StorageCellVM)ListViewStorageCell.SelectedItem;
            if (selectedCelll != null)
            {
                var workWindow = new WorkStorageCell(selectedCelll);
                if (workWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }
    }
}
