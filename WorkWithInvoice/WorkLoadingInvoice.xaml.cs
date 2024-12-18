using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace Pro_Practice.WorkWithInvoice
{
    /// <summary>
    /// Логика взаимодействия для WorkLoadingInvoice.xaml
    /// </summary>
    public partial class WorkLoadingInvoice : Window
    {
        public BochagovaProPracticeContext _context;
        public LoadingInvoice _invoiceLoad;
        public ObservableCollection<LoadingInvoiceDetailsVM> _movementDetails;
        public WorkLoadingInvoice()
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

            EmployeeComboBox.ItemsSource = _context.Employees.ToList();

            CarComboBox.ItemsSource = _context.Cars.ToList();
            CarComboBox.DisplayMemberPath = "NameCar";

            InvoiceComboBox.ItemsSource = _context.Invoices.ToList();
            InvoiceComboBox.DisplayMemberPath = "Period";

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
            Car selectedCar = (Car)CarComboBox.SelectedItem;
            EmployeeVM selectedEmployee = (EmployeeVM)EmployeeComboBox.SelectedItem;

            _invoiceLoad = new LoadingInvoice
            {
                Period = period,
                IdDivision = selectedDivision.IdDivision,
                IdWarehouse = selectedWarehouse.Id,
                IdCar = selectedCar.IdCar,
                IdEmployeeOnLoad = selectedEmployee.Id,
            };

            _context.LoadingInvoices.Add(_invoiceLoad);
            _context.SaveChanges();
            Invoice invoice = (Invoice)InvoiceComboBox.SelectedItem;
            int numberOfSeats = int.Parse(NumberOfSeatsTextBox.Text);

            var detail = new LoadingInvoicesDetail
            {
                IdLoad = _invoiceLoad.IdLoad,
                LineNumber = 1,
                IdInvoice = invoice.IdInvoice,
                NumberOfSeats = numberOfSeats,
            };
            _context.LoadingInvoicesDetails.Add(detail);

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

            if (CarComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Автомобиль' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (EmployeeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Сотрудник на выгрузке' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (InvoiceComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Накладная' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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
        private void WarehouseEmployeeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedDivision = (Division)DivisionComboBox.SelectedItem;
            LoadWarehouseForDivision(selectedDivision, WarehouseComboBox);
            LoadEmployeeForDivision(selectedDivision, EmployeeComboBox);
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

        private void LoadEmployeeForDivision(Division division, System.Windows.Controls.ComboBox employeeComboBox)
        {
            if (division != null)
            {
                employeeComboBox.ItemsSource = _context.Employees
                    .Where(a => a.IdDivision == division.IdDivision)
                    .Select(a => new EmployeeVM(a))
                    .ToList();
                employeeComboBox.DisplayMemberPath = "FullName";
            }
            else
            {
                employeeComboBox.ItemsSource = null;
            }
        }
    }
}
