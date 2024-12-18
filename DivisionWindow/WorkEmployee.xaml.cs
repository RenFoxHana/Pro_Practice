using Microsoft.EntityFrameworkCore;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Pro_Practice.DivisionWindow
{
    public partial class WorkEmployee : Window
    {
        private BochagovaProPracticeContext _context;
        private EmployeeVM _employeeVM;
        private Employee _employee;
        private int selectedDivisionId;
        public WorkEmployee()
        {
            InitializeComponent();
            _context = new BochagovaProPracticeContext(new DbContextOptions<BochagovaProPracticeContext>());
            LoadData();
        }

        public WorkEmployee(EmployeeVM selectedEmployee) : this()
        {
            _employeeVM = selectedEmployee;
            _employee = _context.Employees.FirstOrDefault(e => e.IdEmployee == _employeeVM.Id);

            LastNameTextBox.Text = _employee.LastName;
            FirstNameTextBox.Text = _employee.FirstName;
            PatronymicTextBox.Text = _employee.Patronymic;
            PhoneNumberTextBox.Text =_employee.PhoneNumber;
            LoginTextBox.Text = _employee.Login;
            PasswordBox.Password = _employee.Password;

            DivisionComboBox.SelectedItem = _context.Divisions
                .FirstOrDefault(d => d.IdDivision == _employee.IdDivision);
            RoleComboBox.SelectedItem = _context.UserRoles
                .FirstOrDefault(d => d.IdRole == _employee.IdRole);
        }

        private void LoadData()
        {
            DivisionComboBox.ItemsSource = _context.Divisions.ToList();
            DivisionComboBox.DisplayMemberPath = "NameDivision";

            RoleComboBox.ItemsSource= _context.UserRoles.ToList();
            RoleComboBox.DisplayMemberPath = "NameRole";
        }

        private void PhoneNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }


        private bool AreRequiredFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                MessageBox.Show("Поле 'Фамилия' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                MessageBox.Show("Поле 'Имя' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(PatronymicTextBox.Text))
            {
                MessageBox.Show("Поле 'Отчество' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text))
            {
                MessageBox.Show("Поле 'Номер телефона' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(LoginTextBox.Text))
            {
                MessageBox.Show("Поле 'Логин' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Поле 'Пароль' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            string pattern = @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
            if (!Regex.IsMatch(PasswordBox.Password, pattern))
            {
                MessageBox.Show("Пароль должен содержать минимум 8 символов, включая английский буквы, цифры и специальные символы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (DivisionComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Подразделение' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (RoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Должность' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }


            // Проверка формата номера телефона
            if (!Regex.IsMatch(PhoneNumberTextBox.Text, @"^7\(\d{3}\)\d{7}$"))
            {
                MessageBox.Show("Поле 'Номер телефона' должно быть в формате 7(999)9999999.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            string lastName = LastNameTextBox.Text;
            string firstName = FirstNameTextBox.Text;
            string patronymic = PatronymicTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
            Division selectedDivision = (Division)DivisionComboBox.SelectedItem;
            int selectedDivisionId = selectedDivision.IdDivision;
            UserRole selectedRole = (UserRole)RoleComboBox.SelectedItem;
            int selectedRoleId = selectedRole.IdRole;
          
            if (_employee == null)
            {
                Employee existingLogin = _context.Employees.FirstOrDefault(u => u.Login == login);
                if (existingLogin != null)
                {
                    MessageBox.Show("Логин уже существует!");
                    return;
                }

                var newEmployee = new Employee
                {
                    LastName = lastName,
                    FirstName = firstName,
                    Patronymic = patronymic,
                    PhoneNumber = phoneNumber,
                    IdDivision = selectedDivisionId,
                    IdRole = selectedRoleId,
                    Login = login,
                    Password = password,
                };

                _context.Employees.Add(newEmployee);
            }
            else
            {
                _employee.LastName = lastName;
                _employee.FirstName = firstName;
                _employee.Patronymic = patronymic;
                _employee.PhoneNumber = phoneNumber;
                _employee.IdDivision = selectedDivisionId;
                _employee.IdRole = selectedRoleId;
                _employee.Login = login;
                _employee.Password = password;

                _context.Employees.Update(_employee);
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