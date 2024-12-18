using Microsoft.EntityFrameworkCore;
using Pro_Practice.Models;
using System.Windows;

namespace Pro_Practice.DivisionWindow
{
    public partial class WorkDivision : Window
    {
        private BochagovaProPracticeContext _context;
        private string _divisionName;

        public WorkDivision()
        {
            InitializeComponent();
            _context = new BochagovaProPracticeContext(new DbContextOptions<BochagovaProPracticeContext>());
            LoadData();
        }

        public WorkDivision(string divisionName) : this()
        {
            _divisionName = divisionName;
            LoadDivisionData();
        }

        private void LoadData()
        {
            var employeesInDivision = _context.Employees
                .Where(e => e.IdDivision == GetCurrentDivisionId())
                .Select(e => new
                {
                    e.IdEmployee,
                    FullName = $"{e.FirstName} {e.LastName} {e.Patronymic}"
                })
                .ToList();

            EmployeeComboBox.ItemsSource = employeesInDivision;
            EmployeeComboBox.DisplayMemberPath = "FullName";
            EmployeeComboBox.SelectedValuePath = "IdEmployee"; 
        }

        private int GetCurrentDivisionId()
        {
            var division = _context.Divisions.FirstOrDefault(d => d.NameDivision == _divisionName);
            return division != null ? division.IdDivision : 0;
        }

        private void LoadDivisionData()
        {
            var division = _context.Divisions
                .Include(d => d.IdEmployeeNavigation) 
                .FirstOrDefault(d => d.NameDivision == _divisionName);

            if (division != null)
            {
                NameDivisionTextBox.Text = division.NameDivision;
                LoadData();
                EmployeeComboBox.SelectedValue = division.IdEmployee;
            }
        }
        private bool AreRequiredFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(NameDivisionTextBox.Text))
            {
                MessageBox.Show("Поле 'Наименование' обязательно для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            var division = new Division
            {
                NameDivision = NameDivisionTextBox.Text,
                IdEmployee = (EmployeeComboBox.SelectedItem as dynamic)?.IdEmployee,
                IdDivision = _divisionName == null ? 0 : _context.Divisions.First(d => d.NameDivision == _divisionName).IdDivision
            };

            if (_divisionName == null)
            {
                _context.Divisions.Add(division);
            }
            else
            {
                var existingDivision = _context.Divisions.First(d => d.NameDivision == _divisionName);
                existingDivision.NameDivision = division.NameDivision;
                existingDivision.IdEmployee = division.IdEmployee;
            }

            _context.SaveChanges();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}