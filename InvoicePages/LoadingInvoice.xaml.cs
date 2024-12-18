using Microsoft.EntityFrameworkCore;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using Pro_Practice.WorkWithInvoice;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Pro_Practice.InvoicePages
{
    /// <summary>
    /// Логика взаимодействия для LoadingInvoice.xaml
    /// </summary>
    public partial class LoadingInvoices : Page
    {

        private BochagovaProPracticeContext _context;
        public ObservableCollection<LoadingInvoiceVM> InvoicesLoad { get; set; }

        public LoadingInvoices()
        {
            InitializeComponent();
            DataContext = this;
            InvoicesLoad = new ObservableCollection<LoadingInvoiceVM>();
            LoadInvoices();
            if (App.currentUser.IdRole != 2)
            {
                AddButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadInvoices()
        {
            InvoicesLoad.Clear();
            using (var context = new BochagovaProPracticeContext())
            {
                var invoices = context.LoadingInvoices
                    .Include(i => i.IdWarehouseNavigation)
                    .Include(i => i.IdDivisionNavigation)
                    .Include(i => i.IdCarNavigation)
                    .Include(i => i.IdEmployeeOnLoadNavigation)
                    .ToList()
                    .Select(i => new LoadingInvoiceVM(i))
                    .ToList();

                foreach (var invoice in invoices)
                {
                    InvoicesLoad.Add(invoice);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WorkLoadingInvoice workLoadInvoiceWindow = new WorkLoadingInvoice();
            bool? result = workLoadInvoiceWindow.ShowDialog();
            if (result == true)
            {
                LoadInvoices();
            }
        }
    }
}
