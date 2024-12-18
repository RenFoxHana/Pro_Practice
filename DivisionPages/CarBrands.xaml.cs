using Pro_Practice.DivisionWindow;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Pro_Practice.DivisionPages
{
    /// <summary>
    /// Логика взаимодействия для CarBrands.xaml
    /// </summary>
    public partial class CarBrands : Page
    {
        private BochagovaProPracticeContext _context = new BochagovaProPracticeContext();
        public ObservableCollection<CarBrand> ListCarBrands { get; set; }
        public CarBrands()
        {
            InitializeComponent();
            CarBrandVM viewModel = new CarBrandVM();
            DataContext = viewModel;
            ListCarBrands = new ObservableCollection<CarBrand>(_context.CarBrands.ToList());
            if (App.currentUser.IdRole != 1)
            {
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }

        private CarBrand _selectedCarBrand;
        public CarBrand SelectedCarBrand
        {
            get { return _selectedCarBrand; }
            set
            {
                _selectedCarBrand = value;
            }
        }

        private void ListViewCarBrands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewCarBrands.SelectedItem is CarBrand selectedBrand)
            {
                SelectedCarBrand = selectedBrand;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var workCarBrandWindow = new WorkCarBrand(new BochagovaProPracticeContext());
            workCarBrandWindow.ShowDialog();
            Refresh();

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCarBrand = SelectedCarBrand; 
            var workCarBrandWindow = new WorkCarBrand(new BochagovaProPracticeContext(), selectedCarBrand);
            workCarBrandWindow.ShowDialog();
            Refresh();

        }


        public void Refresh()
        {
            CarBrandVM viewModel = (CarBrandVM)DataContext;
            viewModel.ListCarBrands.Clear();

            var carbrands = _context.CarBrands.ToList();

            foreach (var carbrand in carbrands)
            {
                viewModel.ListCarBrands.Add(carbrand);
            }
        }
    }
}
