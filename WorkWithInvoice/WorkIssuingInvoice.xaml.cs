using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Windows;

namespace Pro_Practice.WorkWithInvoice
{
    /// <summary>
    /// Логика взаимодействия для WorkIssuingInvoice.xaml
    /// </summary>
    public partial class WorkIssuingInvoice : Window
    {

        public BochagovaProPracticeContext _context;
        public IssuingInvoice _invoiceIssuing;
        public WorkIssuingInvoice()
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

            CounterpartieComboBox.ItemsSource = _context.Counterparties.ToList();
            CounterpartieComboBox.DisplayMemberPath = "NameCp";

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
            Invoice selectedInvoice = (Invoice)InvoiceComboBox.SelectedItem;
            WarehouseVM selectedWarehouse = (WarehouseVM)WarehouseComboBox.SelectedItem;
            Counterparty selectedCounterpartie = (Counterparty)CounterpartieComboBox.SelectedItem;
            EmployeeVM selectedEmployee = (EmployeeVM)EmployeeComboBox.SelectedItem;
            int numberOfSeats = int.Parse(NumberOfSeatsTextBox.Text);

            _invoiceIssuing = new IssuingInvoice
            {
                Period = period,
                IdDivision = selectedDivision.IdDivision,
                IdWarehouse = selectedWarehouse.Id,
                IdInvoice = selectedInvoice.IdInvoice,
                IdCounterpartieRecivier = selectedCounterpartie.IdCounterpartie,
                IdEmployeeOnIssue = selectedEmployee.Id,
                NumberOfSeats = numberOfSeats,
            };

            _context.IssuingInvoices.Add(_invoiceIssuing);

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

            if (CounterpartieComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Контрагент-получатель' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (EmployeeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Сотрудник на выдаче' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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