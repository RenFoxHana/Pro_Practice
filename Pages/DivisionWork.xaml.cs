using Pro_Practice.DivisionPages;
using System.Windows.Controls;

namespace Pro_Practice.Pages
{
    /// <summary>
    /// Логика взаимодействия для DivisionWork.xaml
    /// </summary>
    public partial class DivisionWork : Page
    {
        public DivisionWork()
        {
            InitializeComponent();
        }

        private void ButtonCarBrands_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new CarBrands();
        }
        private void ButtonCar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new Cars();
        }
        private void ButtonAdress_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new Adressess();
        }
        private void ButtonDivision_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new Division();
        }
        private void ButtonWarehouse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new Warehouse();
        }
        private void ButtonEmployees_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new Employees();
        }
        private void ButtonStorageCells_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new StorageCells();
        }
    }
}
