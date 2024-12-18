using Microsoft.EntityFrameworkCore;
using Pro_Practice.DivisionWindow;
using Pro_Practice.Models;
using System.Windows;
using System.Windows.Controls;

namespace Pro_Practice.DivisionPages
{
    /// <summary>
    /// Логика взаимодействия для Division.xaml
    /// </summary>
    public partial class Division : Page
    {
        private BochagovaProPracticeContext _context;
        public Division()
        {
            InitializeComponent();
            _context = new BochagovaProPracticeContext(new DbContextOptions<BochagovaProPracticeContext>());
            LoadData();
            if (App.currentUser.IdRole != 1)
            {
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadData()
        {
            var divisions = _context.Divisions
                .Select(d => new
                {
                    d.NameDivision,
                    EmployeeFullName = d.IdEmployeeNavigation != null ? $"{d.IdEmployeeNavigation.LastName} {d.IdEmployeeNavigation.FirstName} {d.IdEmployeeNavigation.Patronymic}" : "Не назначено",
                    Address = $"{d.Addresses.FirstOrDefault().Region} {d.Addresses.FirstOrDefault().District} {d.Addresses.FirstOrDefault().City} {d.Addresses.FirstOrDefault().Locality} " +
                    $"{d.Addresses.FirstOrDefault().Street} {d.Addresses.FirstOrDefault().HouseOrLandPlot} {d.Addresses.FirstOrDefault().Room}"
                })
                .ToList();

            ListViewDivision.ItemsSource = divisions;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var workWindow = new WorkDivision();
            workWindow.ShowDialog();
            LoadData();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDivision = ListViewDivision.SelectedItem as dynamic;
            if (selectedDivision != null)
            {
                var workWindow = new WorkDivision(selectedDivision.NameDivision);
                workWindow.ShowDialog();
                LoadData();
            }
        }
    }
}
