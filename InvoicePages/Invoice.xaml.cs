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
    /// Логика взаимодействия для Invoice.xaml
    /// </summary>
    public partial class Invoice : Page
    {
        private BochagovaProPracticeContext _context;
        public ObservableCollection<InvoiceVM> InvoicesList { get; set; }

        public Invoice()
        {
            InitializeComponent();
            DataContext = this;
            InvoicesList = new ObservableCollection<InvoiceVM>();
            LoadInvoices();
            if (App.currentUser.IdRole != 2)
            {
                AddButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadInvoices()
        {
            InvoicesList.Clear();
            using (var context = new BochagovaProPracticeContext())
            {
                var invoices = context.Invoices
                    .Include(i => i.IdCounterpartieSenderNavigation)
                        .ThenInclude(c => c.Addresses)
                    .Include(i => i.IdCounterpartieRecipientNavigation)
                        .ThenInclude(c => c.Addresses)
                    .Include(i => i.IdTypeCargoNavigation)
                    .Include(i => i.IdDivisionSenderNavigation)
                        .ThenInclude(d => d.Employees)
                    .Include(i => i.IdDivisionRecipientNavigation)
                    .Include(i => i.IdWarehouseSenderNavigation)
                    .ToList()
                    .Select(i => new InvoiceVM(i))
                    .ToList();

                foreach (var invoice in invoices)
                {
                    InvoicesList.Add(invoice);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WorkInvoice workInvoiceWindow = new WorkInvoice();
            bool? result = workInvoiceWindow.ShowDialog();
            if (result == true)
            {
                LoadInvoices();
            }
        }

        //private void EditButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedInvoice = (InvoiceVM)InvoiceListView.SelectedItem;
        //    if (selectedInvoice != null)
        //    {
        //        WorkInvoice workInvoiceWindow = new WorkInvoice(selectedInvoice);
        //        bool? result = workInvoiceWindow.ShowDialog();
        //        if (result == true)
        //        {
        //            LoadInvoices();
        //        }
        //    }
        //}
    }
}
