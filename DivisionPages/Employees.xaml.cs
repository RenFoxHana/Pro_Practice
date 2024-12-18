using Microsoft.EntityFrameworkCore;
using Pro_Practice.DivisionWindow;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Pro_Practice.DivisionPages
{
    /// <summary>
    /// Логика взаимодействия для Employees.xaml
    /// </summary>
    public partial class Employees : Page
    {
        private BochagovaProPracticeContext _context = new BochagovaProPracticeContext();
        public ObservableCollection<EmployeeVM> ListEmployee { get; set; }

        public Employees()
        {
            InitializeComponent();
            DataContext = this;
            ListEmployee = new ObservableCollection<EmployeeVM>();
            LoadData();
            if (App.currentUser.IdRole != 1)
            {
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadData()
        {
            ListEmployee.Clear();
            using (var context = new BochagovaProPracticeContext())
            {
                var employees = context.Employees
                    .Include(e => e.IdDivisionNavigation)
                    .Include(e => e.IdRoleNavigation)
                    .ToList()
                    .Select(e => new EmployeeVM(e))
                    .ToList();

                foreach (var employeeVM in employees)
                {
                    ListEmployee.Add(employeeVM);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var workWindow = new WorkEmployee();
            if (workWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeVM selectedEmployee = (EmployeeVM)ListViewEmployees.SelectedItem;
            if (selectedEmployee != null)
            {
                var workWindow = new WorkEmployee(selectedEmployee);
                if (workWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }
    }
}