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
    /// Логика взаимодействия для Cars.xaml
    /// </summary>
    public partial class Cars : Page
    {
        private BochagovaProPracticeContext _context = new BochagovaProPracticeContext();

        public ObservableCollection<CarVM> ListCar { get; set; }

        public Cars()
        {
            InitializeComponent();
            DataContext = this;
            ListCar = new ObservableCollection<CarVM>();
            LoadData();
            if (App.currentUser.IdRole != 1)
            {
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadData()
        {
            ListCar.Clear();
            using (var context = new BochagovaProPracticeContext())
            {
                var cars = context.Cars
                    .Include(c => c.IdCarBrandNavigation)
                    .Include(c => c.IdEmployeeNavigation)
                    .ToList()
                    .Select(w => new CarVM(w))
                    .ToList();

                foreach (var carVM in cars)
                {
                    ListCar.Add(carVM);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var workWindow = new WorkCar();
            if (workWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            CarVM selectedCar = (CarVM)ListViewCar.SelectedItem;
            if (selectedCar != null)
            {
                var workWindow = new WorkCar(selectedCar);
                if (workWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }
    }
}
