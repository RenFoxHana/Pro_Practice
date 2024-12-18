using Microsoft.EntityFrameworkCore;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace Pro_Practice.DivisionWindow
{
    /// <summary>
    /// Логика взаимодействия для WorkCar.xaml
    /// </summary>
    public partial class WorkCar : Window
    {
        private BochagovaProPracticeContext _context;
        private CarVM _carViewModel;
        private Car _car;

        public WorkCar()
        {
            InitializeComponent();
            _context = new BochagovaProPracticeContext();
            LoadData();
        }

        public WorkCar(CarVM carViewModel) : this()
        {
            _carViewModel = carViewModel;
            _car = _context.Cars.FirstOrDefault(w => w.IdCar == _carViewModel.IdCar);

            if (_car != null)
            {
                CarNameTextBox.Text = _car.NameCar;
                LicensePlateTextBox.Text = _car.LicensePlateNumber;

                CarBrandComboBox.SelectedItem = _context.CarBrands
                    .FirstOrDefault(d => d.IdCarBrand == _car.IdCarBrand);

                var employee = _context.Employees.FirstOrDefault(s => s.IdEmployee == _car.IdEmployee);
                if (employee != null)
                {
                    var employees = _context.Employees
                        .Include(d => d.IdDivisionNavigation)
                        .Select(e => new EmployeeVM(e))
                        .ToList();

                    var selectedEmployee = employees.FirstOrDefault(emp => emp.Id == employee.IdEmployee);
                    EmployeeComboBox.ItemsSource = employees;
                    EmployeeComboBox.SelectedItem = selectedEmployee;
                }
            }
        }

        private void LoadData()
        {
            EmployeeComboBox.ItemsSource = _context.Employees
                .Include(d => d.IdDivisionNavigation)
                .Select(e => new EmployeeVM(e))
                .ToList();

            EmployeeComboBox.DisplayMemberPath = "FullName";
            CarBrandComboBox.ItemsSource = _context.CarBrands.ToList();
            CarBrandComboBox.DisplayMemberPath = "NameBrand";
        }

        private void LoadEmployeeData()
        {
            var car = _context.Cars.FirstOrDefault(d => d.IdEmployee == _car.IdEmployee);
            if (car != null)
            {
                EmployeeComboBox.SelectedItem = car.IdEmployeeNavigation;
            }
        }

        private bool AreRequiredFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(LicensePlateTextBox.Text))
            {
                MessageBox.Show("Поле 'Госномер' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (EmployeeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Водитель' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CarBrandComboBox.SelectedItem == null)
            {
                MessageBox.Show("Поле 'Марка' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Проверка формата государственного номера
            if (!Regex.IsMatch(LicensePlateTextBox.Text, @"^[A-Z] \d{3} [A-Z]{2} [A-Z]{3}$"))
            {
                MessageBox.Show("Поле 'Госномер' должно быть в формате A 999 AA AAA.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            string carname = CarNameTextBox.Text;
            string platenumber = LicensePlateTextBox.Text;

            EmployeeVM selectedEmployeeVM = (EmployeeVM)EmployeeComboBox.SelectedItem;
            int selectedEmployeeId = selectedEmployeeVM?.Id ?? default;

            CarBrand selectedBrand = (CarBrand)CarBrandComboBox.SelectedItem;
            int selectedBrandId = selectedBrand.IdCarBrand;

            if (_car == null)
            {
                Car existingNumber = _context.Cars.FirstOrDefault(u => u.LicensePlateNumber == platenumber);
                if (existingNumber != null)
                {
                    MessageBox.Show("Госномер уже существует!");
                    return;
                }
                var newCar = new Car
                {
                    NameCar = $"{selectedBrand.NameBrand} {platenumber}",
                    LicensePlateNumber = platenumber,
                    IdCarBrand = selectedBrandId,
                    IdEmployee = selectedEmployeeId
                };

                _context.Cars.Add(newCar);
            }
            else
            {
                _car.NameCar = $"{selectedBrand.NameBrand} {platenumber}";
                _car.LicensePlateNumber = platenumber;
                _car.IdCarBrand = selectedBrandId;
                _car.IdEmployee = selectedEmployeeId;

                _context.Cars.Update(_car);
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