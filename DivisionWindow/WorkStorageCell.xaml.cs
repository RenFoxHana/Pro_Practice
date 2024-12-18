using Microsoft.EntityFrameworkCore;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace Pro_Practice.DivisionWindow
{
    /// <summary>
    /// Логика взаимодействия для WorkStorageCell.xaml
    /// </summary>
    public partial class WorkStorageCell : Window
    {
        private BochagovaProPracticeContext _context;
        private StorageCellVM _cellVM;
        private StorageCell _cell;
        private int selectedWarehouseId;
        public WorkStorageCell()
        {
            InitializeComponent();
            _context = new BochagovaProPracticeContext(new DbContextOptions<BochagovaProPracticeContext>());
            LoadData();
        }

        public WorkStorageCell(StorageCellVM selectedCell) : this()
        {
            _cellVM = selectedCell;
            _cell = _context.StorageCells.FirstOrDefault(e => e.IdStorageCell == _cellVM.IdStorageCell);

            NameCellTextBox.Text = _cell.NameCell;

            WarehouseComboBox.SelectedItem = _context.Warehouses
                .FirstOrDefault(d => d.IdWarehouse == _cell.IdWarehouse);
        }

        private void LoadData()
        {
            WarehouseComboBox.ItemsSource = _context.Warehouses.ToList();
            WarehouseComboBox.DisplayMemberPath = "NameWarehouse";
        }

        private bool AreRequiredFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(NameCellTextBox.Text))
            {
                MessageBox.Show("Поле 'Наименование' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (WarehouseComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Склад' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            string nameCell = NameCellTextBox.Text;
            Warehouse selectedWarehouse = (Warehouse)WarehouseComboBox.SelectedItem;
            int selectedWarehouseId = selectedWarehouse.IdWarehouse;

            if (_cell == null)
            {
                var newCell = new StorageCell
                {
                    NameCell = nameCell,
                    IdWarehouse = selectedWarehouseId
                };

                _context.StorageCells.Add(newCell);
            }
            else
            {
                _cell.NameCell = nameCell;
                _cell.IdWarehouse = selectedWarehouseId;

                _context.StorageCells.Update(_cell);
            }

            _context.SaveChanges();

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
