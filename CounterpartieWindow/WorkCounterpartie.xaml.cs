using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;

namespace Pro_Practice.CounterpartieWindow
{
    public partial class WorkCounterpartie : Window
    {
        private BochagovaProPracticeContext _context;
        private Counterparty _counterparty;
        private CounterpartieVM _counterpartieVM;

        // Конструктор для создания нового контрагента
        public WorkCounterpartie()
        {
            InitializeComponent();
            _context = new BochagovaProPracticeContext();
            LoadCounterpartyTypes();
        }

        // Конструктор для редактирования существующего контрагента
        public WorkCounterpartie(CounterpartieVM counterparty) : this()
        {
            _counterpartieVM = counterparty;
            _counterparty = _context.Counterparties.FirstOrDefault(w => w.NameCp == _counterpartieVM.NameCp); ;
            NameTextBox.Text = _counterparty.NameCp;
            TypeComboBox.SelectedItem = _context.TypesOfCounterparties.Find(_counterparty.IdTypeCp);
            PassportTextBox.Text = _counterparty.PassportData;
            TaxIdTextBox.Text = _counterparty.TaxpayerIdentificationNumber;
        }

        private void LoadCounterpartyTypes()
        {
            TypeComboBox.ItemsSource = _context.TypesOfCounterparties.ToList();
            TypeComboBox.DisplayMemberPath = "NameType";
        }

        private void TypeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TypeComboBox.SelectedItem is TypesOfCounterparty selectedType)
            {
                PassportTextBox.Visibility = selectedType.IdTypeCp == 1 ? Visibility.Visible : Visibility.Collapsed;
                TaxIdTextBox.Visibility = selectedType.IdTypeCp == 2 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private bool AreRequiredFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Поле 'Имя' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (TypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Тип контрагента' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (TypeComboBox.SelectedItem is TypesOfCounterparty selectedType)
            {
                if (selectedType.IdTypeCp == 1)
                {
                    if (string.IsNullOrWhiteSpace(PassportTextBox.Text))
                    {
                        MessageBox.Show("Поле 'Паспортные данные' обязательно для заполнения для физических лиц.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }

                    if (!Regex.IsMatch(PassportTextBox.Text, @"^\d{2} \d{2} \d{6}$"))
                    {
                        MessageBox.Show("Поле 'Паспортные данные' должно быть в формате XX XX XXXXXX.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                }

                if (selectedType.IdTypeCp == 2)
                {
                    if (string.IsNullOrWhiteSpace(TaxIdTextBox.Text))
                    {
                        MessageBox.Show("Поле 'ИНН' обязательно для заполнения для юридических лиц.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }

                    if (!Regex.IsMatch(TaxIdTextBox.Text, @"^\d{10}$"))
                    {
                        MessageBox.Show("Поле 'ИНН' должно содержать 10 цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                }
            }

            return true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!AreRequiredFieldsFilled())
            {
                return;
            }

            string name = NameTextBox.Text;
            TypesOfCounterparty selectedType = (TypesOfCounterparty)TypeComboBox.SelectedItem;

            string passportData = PassportTextBox.Visibility == Visibility.Visible ? PassportTextBox.Text : null;
            string taxNumber = TaxIdTextBox.Visibility == Visibility.Visible ? TaxIdTextBox.Text : null;

            if (_counterparty == null)
            {
                var newCounterparty = new Counterparty
                {
                    NameCp = name,
                    IdTypeCp = selectedType.IdTypeCp,
                    PassportData = passportData,
                    TaxpayerIdentificationNumber = taxNumber
                };

                _context.Counterparties.Add(newCounterparty);
            }
            else
            {
                _counterparty.NameCp = name;
                _counterparty.IdTypeCp = selectedType.IdTypeCp;
                _counterparty.PassportData = passportData;
                _counterparty.TaxpayerIdentificationNumber = taxNumber;

                _context.Counterparties.Update(_counterparty);
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