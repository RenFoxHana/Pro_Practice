using Pro_Practice.CounterpartiePages;
using Pro_Practice.DivisionPages;
using Pro_Practice.InvoicePages;
using System.Windows.Controls;

namespace Pro_Practice.Pages
{
    /// <summary>
    /// Логика взаимодействия для CounterpertieWork.xaml
    /// </summary>
    public partial class CounterpartieWork : Page
    {
        public CounterpartieWork()
        {
            InitializeComponent();
        }

        private void ButtonAdress_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new Adressess();
        }
        private void ButtonCounterpartie_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new Counterparties();
        }
        private void ButtonTypeCP_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new TypeCounterpartie();
        }

    }
}
