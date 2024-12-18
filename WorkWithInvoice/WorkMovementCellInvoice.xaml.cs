using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;

namespace Pro_Practice.WorkWithInvoice
{
    /// <summary>
    /// Логика взаимодействия для WorkMovementCellInvoice.xaml
    /// </summary>
    public partial class WorkMovementCellInvoice : Window
    {
        public BochagovaProPracticeContext _context;
        public InvoiceCellMovement _invoiceCellMovement;
        public ObservableCollection<InvoiceCellMovementsDetailsVM> _movementDetails;
        public WorkMovementCellInvoice()
        {
            InitializeComponent();
            _context = new BochagovaProPracticeContext();
            DataContext = this;
            LoadData();
        }

        private void LoadData()
        {
            DivisionComboBox.ItemsSource = _context.Divisions.ToList();
            DivisionComboBox.DisplayMemberPath = "NameDivision";

            WarehouseComboBox.ItemsSource = _context.Warehouses.ToList();
            WarehouseComboBox.DisplayMemberPath = "NameWarehouse";

            InvoiceComboBox.ItemsSource = _context.Invoices.ToList();
            InvoiceComboBox.DisplayMemberPath = "Period";

            CellToComboBox.ItemsSource = _context.StorageCells.ToList();
            CellToComboBox.DisplayMemberPath = "NameCell";

            CellFromComboBox.ItemsSource = _context.StorageCells.ToList();
            CellFromComboBox.DisplayMemberPath = "NameCell";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!AreRequiredFieldsFilled())
            {
                return;
            }

            DateTime period = (DateTime)DateTimePicker.Value;
            Division selectedDivision = (Division)DivisionComboBox.SelectedItem;
            WarehouseVM selectedWarehouse = (WarehouseVM)WarehouseComboBox.SelectedItem;

            _invoiceCellMovement = new InvoiceCellMovement
            {
                Period = period,
                IdDivision = selectedDivision.IdDivision,
                IdWarehouse = selectedWarehouse.Id
            };

            _context.InvoiceCellMovements.Add(_invoiceCellMovement);
            _context.SaveChanges();
            Invoice invoice = (Invoice)InvoiceComboBox.SelectedItem;
            StorageCell cell = (StorageCell)CellFromComboBox.SelectedItem;
            StorageCell cell2 = (StorageCell)CellToComboBox.SelectedItem;
            int numberOfSeats = int.Parse(NumberOfSeatsTextBox.Text);

            var detail = new InvoiceCellMovementsDetail
            {
                IdMovement = _invoiceCellMovement.IdMovement,
                LineNumber = 1,
                IdInvoice = invoice.IdInvoice,
                IdStorageCellFrom = cell.IdStorageCell,
                IdStorageCellTo = cell2.IdStorageCell,
                NumberOfSeats = numberOfSeats,
            };
            _context.InvoiceCellMovementsDetails.Add(detail);

            DialogResult = true;

            _context.SaveChanges();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool AreRequiredFieldsFilled()
        {
            if (DateTimePicker.Value == null)
            {
                MessageBox.Show("Поле 'Период' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (DivisionComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Подразделение' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (WarehouseComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Склад' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (InvoiceComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Накладная' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CellFromComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Ячейка откуда' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CellToComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Ячейка куда' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(NumberOfSeatsTextBox.Text) ||
                !int.TryParse(NumberOfSeatsTextBox.Text, out int numberOfSeats) ||
                 numberOfSeats <= 0)
            {
                MessageBox.Show("Поле 'Количество мест' обязательно для заполнения и должно быть положительным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            var selectedInvoice = (Invoice)InvoiceComboBox.SelectedItem;
            if (selectedInvoice != null)
            {
                int expectedNumberOfSeats = _context.Invoices
                    .Where(detail => detail.IdInvoice == selectedInvoice.IdInvoice)
                    .Sum(detail => detail.NumberOfSeats);

                if (numberOfSeats != expectedNumberOfSeats)
                {
                    MessageBox.Show($"Количество мест должно быть равно {expectedNumberOfSeats}.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            return true;
        }

        private void WarehouseComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedDivision = (Division)DivisionComboBox.SelectedItem;
            LoadWarehouseForDivision(selectedDivision, WarehouseComboBox);
        }
        private void LoadWarehouseForDivision(Division division, System.Windows.Controls.ComboBox warehouseComboBox)
        {
            if (division != null)
            {
                warehouseComboBox.ItemsSource = _context.Warehouses
                    .Where(a => a.IdDivision == division.IdDivision)
                    .Select(a => new WarehouseVM(a))
                    .ToList();
                warehouseComboBox.DisplayMemberPath = "NameWarehouse";
            }
            else
            {
                warehouseComboBox.ItemsSource = null;
            }
        }

        private void CellComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedWarehouse = (Warehouse)WarehouseComboBox.SelectedItem;
            LoadCellForWarehouse(selectedWarehouse, CellFromComboBox);
            LoadCellForWarehouse(selectedWarehouse, CellToComboBox);
        }
        private void LoadCellForWarehouse(Warehouse warehouse, System.Windows.Controls.ComboBox storageComboBox)
        {
            if (warehouse != null)
            {
                storageComboBox.ItemsSource = _context.StorageCells
                    .Where(a => a.IdStorageCell == warehouse.DefaultCell)
                    .Select(a => new StorageCellVM(a))
                    .ToList();
                storageComboBox.DisplayMemberPath = "NameCell";
            }
            else
            {
                storageComboBox.ItemsSource = null;
            }
        }
    }
}
