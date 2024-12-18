using Pro_Practice.InvoicePages;
using System.Windows.Controls;

namespace Pro_Practice.Pages
{
    /// <summary>
    /// Логика взаимодействия для InvoiceWork.xaml
    /// </summary>
    public partial class InvoiceWork : Page
    {
        public InvoiceWork()
        {
            InitializeComponent();
        }

        private void ButtonTypeCargo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new TypeCargos();
        }
        private void ButtonUnloadInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new UnloadInvoice();
        }
        private void ButtonIssuingInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new IssuingInvoice();
        }
        private void ButtonCargoInCells_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new CargoInCells();
        }
        private void ButtonLoadingInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new LoadingInvoices();
        }
        private void ButtonInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new Invoice();
        }
        private void ButtonMovementInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new InvoiceCellMovements();
        }
        private void ButtonDocumentType_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainFrame.Content = new DocumentTypes();
        }
        
    }
}
