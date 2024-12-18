using Pro_Practice.Pages;
using System.Windows;

namespace Pro_Practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (App.currentUser.IdRole == 2)
            {
                ButtonDivision.Visibility = Visibility.Collapsed;
                ButtonCounterpartie.Margin = new Thickness(130, 15, 0, 0);
                ButtonInvoice.Margin = new Thickness(130, 15, 0, 0);
                ButtonExit.Margin = new Thickness(150, 15, 0, 0);
            }
        }

        private void ButtonDivision_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DivisionWork();
        }

        private void ButtonCounterpartie_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CounterpartieWork();
        }

        private void ButtonInvoice_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new InvoiceWork();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            Close();
        }
    }
}