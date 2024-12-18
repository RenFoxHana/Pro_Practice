using Microsoft.EntityFrameworkCore;
using Pro_Practice.Models;
using Pro_Practice.Pages;
using Pro_Practice.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace Pro_Practice.WorkWithInvoice
{
    /// <summary>
    /// Логика взаимодействия для WorkInvoice.xaml
    /// </summary>
    public partial class WorkInvoice : Window
    {
        private BochagovaProPracticeContext _context;
        private InvoiceVM _invoiceViewModel;
        private Models.Invoice _invoice;
        public ObservableCollection<Address> ListAddress { get; set; }
        public WorkInvoice()
        {
            InitializeComponent();
            _context = new BochagovaProPracticeContext();
            LoadData();
        }

        public WorkInvoice(InvoiceVM invoiceViewModel) : this()
        {
            _invoiceViewModel = invoiceViewModel;
            _invoice = _context.Invoices.FirstOrDefault(w => w.IdInvoice == _invoiceViewModel.IdInvoice);

            if (_invoice != null)
            {
                PeriodDatePicker.Value = _invoice.Period;
                CpSenderComboBox.SelectedItem = _invoice.IdCounterpartieSenderNavigation; // Обновляем контрагент-отправитель
                CpAddressSenderComboBox.SelectedItem = _invoice.IdSendersAddressNavigation; // Адрес отправления
                CpRecipientComboBox.SelectedItem = _invoice.IdCounterpartieRecipientNavigation; // Контрагент-получатель
                CpAddressRecipientComboBox.SelectedItem = _invoice.IdRecipientsAddressNavigation; // Адрес доставки
                DivisionSenderComboBox.SelectedItem = _invoice.IdDivisionSenderNavigation; // Подразделение отправитель
                WarehouseSenderComboBox.SelectedItem = _invoice.IdWarehouseSenderNavigation; // Склад отправитель
                DivisionRecipientComboBox.SelectedItem = _invoice.IdDivisionRecipientNavigation; // Подразделение получатель
                EmployeeComboBox.SelectedItem = _invoice.IdEmployeeAtAcceptanceNavigation; // Сотрудник на приемке
                TypeCargoComboBox.SelectedItem = _invoice.IdTypeCargoNavigation; // Вид груза
                NumberOfSeatsTextBox.Text = _invoice.NumberOfSeats.ToString(); // Количество мест
                WeightTextBox.Text = _invoice.WeightCargo.ToString(); // Вес
                VolumeTextBox.Text = _invoice.VolumeCargo.ToString(); // Объем
            }
        }

        private void LoadData()
        {
            EmployeeComboBox.ItemsSource = _context.Employees
                 .Include(d => d.IdDivisionNavigation)
                 .Select(e => new EmployeeVM(e))
                 .ToList();
            EmployeeComboBox.DisplayMemberPath = "FullName";

            CpSenderComboBox.ItemsSource = _context.Counterparties.ToList();
            CpSenderComboBox.DisplayMemberPath = "NameCp";

            CpRecipientComboBox.ItemsSource = _context.Counterparties.ToList();
            CpRecipientComboBox.DisplayMemberPath = "NameCp";

            DivisionSenderComboBox.ItemsSource = _context.Divisions.ToList();
            DivisionSenderComboBox.DisplayMemberPath = "NameDivision";

            DivisionRecipientComboBox.ItemsSource = _context.Divisions.ToList();
            DivisionRecipientComboBox.DisplayMemberPath = "NameDivision";

            WarehouseSenderComboBox.ItemsSource = _context.Warehouses.ToList();
            WarehouseSenderComboBox.DisplayMemberPath = "NameWarehouse";

            ListAddress = new ObservableCollection<Address>(_context.Addresses.ToList());

            CpAddressSenderComboBox.ItemsSource = _context.Addresses.ToList();
            CpAddressRecipientComboBox.ItemsSource = _context.Addresses.ToList();

            TypeCargoComboBox.ItemsSource = _context.TypesOfCargos.ToList(); 
            TypeCargoComboBox.DisplayMemberPath = "NameTypeCargo"; 
        }

        private void CpSenderComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedCounterparty = (Counterparty)CpSenderComboBox.SelectedItem;
            LoadAddressesForCounterparty(selectedCounterparty, CpAddressSenderComboBox);
        }

        private void CpRecipientComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedCounterparty = (Counterparty)CpRecipientComboBox.SelectedItem;
            LoadAddressesForCounterparty(selectedCounterparty, CpAddressRecipientComboBox);
        }

        private void EmployeeWarehouseComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedDivision = (Division)DivisionSenderComboBox.SelectedItem;
            LoadEmployeesForDivision(selectedDivision, EmployeeComboBox);
            LoadWarehouseForDivision(selectedDivision, WarehouseSenderComboBox);
        }

        private void LoadAddressesForCounterparty(Counterparty counterparty, System.Windows.Controls.ComboBox addressComboBox)
        {
            if (counterparty != null)
            {
                addressComboBox.ItemsSource = _context.Addresses
                    .Where(a => a.IdCounterpartie == counterparty.IdCounterpartie)
                    .Select(a => new AddressVM(a))
                    .ToList();
                addressComboBox.DisplayMemberPath = "FullAddress"; 
            }
            else
            {
                addressComboBox.ItemsSource = null; 
            }
        }

        private void LoadEmployeesForDivision(Division division, System.Windows.Controls.ComboBox employeeComboBox)
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

        private bool AreRequiredFieldsFilled()
        {
            if (PeriodDatePicker.Value == null)
            {
                MessageBox.Show("Поле 'Период' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CpSenderComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Контрагент-отправитель' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CpAddressSenderComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Адрес отправления' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CpRecipientComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Контрагент-получатель' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CpAddressRecipientComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Адрес доставки' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (DivisionSenderComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Подразделение отправитель' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (WarehouseSenderComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Склад отправитель' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (DivisionRecipientComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Подразделение получатель' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (EmployeeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Сотрудник на приемке' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (TypeCargoComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Вид груза' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(NumberOfSeatsTextBox.Text))
            {
                MessageBox.Show("Поле 'Количество мест' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(WeightTextBox.Text))
            {
                MessageBox.Show("Поле 'Вес' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(VolumeTextBox.Text))
            {
                MessageBox.Show("Поле 'Объем' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Проверка корректности введенных данных
            if (!int.TryParse(NumberOfSeatsTextBox.Text, out _))
            {
                MessageBox.Show("Поле 'Количество мест' должно быть целым числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(WeightTextBox.Text, out _))
            {
                MessageBox.Show("Поле 'Вес' должно быть десятичным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(VolumeTextBox.Text, out _))
            {
                MessageBox.Show("Поле 'Объем' должно быть десятичным числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            var selectedSender = (Counterparty)CpSenderComboBox.SelectedItem;
            var selectedRecipient = (Counterparty)CpRecipientComboBox.SelectedItem;
            var selectedAddressSender = (AddressVM)CpAddressSenderComboBox.SelectedItem;
            var selectedAddressRecipient = (AddressVM)CpAddressRecipientComboBox.SelectedItem;
            var selectedDivisionSender = (Division)DivisionSenderComboBox.SelectedItem;
            var selectedWarehouseSender = (WarehouseVM)WarehouseSenderComboBox.SelectedItem;
            var selectedDivisionRecipient = (Division)DivisionRecipientComboBox.SelectedItem;
            var selectedEmployee = (EmployeeVM)EmployeeComboBox.SelectedItem;
            var selectedTypeCargo = (TypesOfCargo)TypeCargoComboBox.SelectedItem;

            int senderId = selectedSender.IdCounterpartie; 
            int recipientId = selectedRecipient.IdCounterpartie; 
            int addressSenderId = selectedAddressSender.IdAddress; 
            int addressRecipientId = selectedAddressRecipient.IdAddress; 
            int divisionSenderId = selectedDivisionSender.IdDivision; 
            int warehouseSenderId = selectedWarehouseSender.Id;
            int divisionRecipientId = selectedDivisionRecipient.IdDivision; 
            int employeeId = selectedEmployee.Id; 
            int typeCargoId = selectedTypeCargo.IdTypeCargo;

            int numberOfSeats = int.Parse(NumberOfSeatsTextBox.Text);
            decimal weight = decimal.Parse(WeightTextBox.Text);
            decimal volume = decimal.Parse(VolumeTextBox.Text);
            DateTime date = (DateTime)PeriodDatePicker.Value;

            if (_invoice == null)
            {
                var newInvoice = new Models.Invoice
                {
                    Period = date,
                    IdCounterpartieSender = senderId,
                    IdSendersAddress = addressSenderId,
                    IdCounterpartieRecipient = recipientId,
                    IdRecipientsAddress = addressRecipientId,
                    IdDivisionSender = divisionSenderId,
                    IdWarehouseSender = warehouseSenderId,
                    IdDivisionRecipient = divisionRecipientId,
                    IdEmployeeAtAcceptance = employeeId,
                    IdTypeCargo = typeCargoId,
                    NumberOfSeats = numberOfSeats,
                    WeightCargo = weight,
                    VolumeCargo = volume,
                };

                _context.Invoices.Add(newInvoice);
            }
            else
            {
                _invoice.Period = date;
                _invoice.IdCounterpartieSender = senderId;
                _invoice.IdSendersAddress = addressSenderId;
                _invoice.IdCounterpartieRecipient = recipientId;
                _invoice.IdRecipientsAddress = addressRecipientId;
                _invoice.IdDivisionSender = divisionSenderId;
                _invoice.IdWarehouseSender = warehouseSenderId;
                _invoice.IdDivisionRecipient = divisionRecipientId;
                _invoice.IdEmployeeAtAcceptance = employeeId;
                _invoice.IdTypeCargo = typeCargoId;
                _invoice.NumberOfSeats = numberOfSeats;
                _invoice.WeightCargo = weight;
                _invoice.VolumeCargo = volume;

                _context.Invoices.Update(_invoice);
            }

            _context.SaveChanges();
            DialogResult = true;
             Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}