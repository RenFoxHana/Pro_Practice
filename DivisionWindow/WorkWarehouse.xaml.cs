using Microsoft.EntityFrameworkCore;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace Pro_Practice.DivisionWindow
{
    public partial class WorkWarehouse : Window
    {
        private WarehouseVM _warehouseVM;
        private BochagovaProPracticeContext _context;
        private Warehouse _warehouse;

        public WorkWarehouse()
        {
            InitializeComponent();
            _context = new BochagovaProPracticeContext(new DbContextOptions<BochagovaProPracticeContext>());
            LoadData();
        }

        public WorkWarehouse(WarehouseVM warehouseVM) : this()
        {
            _warehouseVM = warehouseVM;
            _warehouse = _context.Warehouses.FirstOrDefault(w => w.IdWarehouse == _warehouseVM.Id);

            WarehouseNameTextBox.Text = _warehouse.NameWarehouse;

            DivisionComboBox.SelectedItem = _context.Divisions
                .FirstOrDefault(d => d.IdDivision == _warehouse.IdDivision);
            StorageCellComboBox.SelectedItem = _context.StorageCells
                .FirstOrDefault(s => s.IdStorageCell == _warehouse.DefaultCell);
        }

        private void LoadData()
        {
            DivisionComboBox.ItemsSource = _context.Divisions.ToList();
            DivisionComboBox.DisplayMemberPath = "NameDivision";
            StorageCellComboBox.ItemsSource = _context.StorageCells.ToList();
            StorageCellComboBox.DisplayMemberPath = "NameCell";
        }

        private bool AreRequiredFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(WarehouseNameTextBox.Text))
            {
                MessageBox.Show("Поле 'Наименование' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (DivisionComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Подразделение' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            string warehousename = WarehouseNameTextBox.Text;
            Division selectedDivision = (Division)DivisionComboBox.SelectedItem;
            int selectedDivisionId = selectedDivision.IdDivision;
            StorageCell selectedCell = (StorageCell)StorageCellComboBox.SelectedItem;
            int? selectedCellId = selectedCell?.IdStorageCell;

            if (_warehouse == null)
            {
                var newWarehouse = new Warehouse
                {
                    NameWarehouse = warehousename,
                    IdDivision = selectedDivisionId,
                    DefaultCell = selectedCellId
                };

                _context.Warehouses.Add(newWarehouse);
            }
            else
            {
                _warehouse.NameWarehouse = warehousename;
                _warehouse.DefaultCell = selectedCellId;
                _warehouse.IdDivision = selectedDivisionId;

                _context.Warehouses.Update(_warehouse);
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