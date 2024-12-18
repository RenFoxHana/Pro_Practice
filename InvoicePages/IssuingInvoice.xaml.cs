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
    /// Логика взаимодействия для IssuingInvoice.xaml
    /// </summary>
    public partial class IssuingInvoice : Page
    {
        private BochagovaProPracticeContext _context;
        public ObservableCollection<IssuingInvoiceVM> InvoicesIssue { get; set; }

        public IssuingInvoice()
        {
            InitializeComponent();
            DataContext = this;
            InvoicesIssue = new ObservableCollection<IssuingInvoiceVM>();
            LoadInvoices();
            if (App.currentUser.IdRole != 2)
            {
                AddButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadInvoices()
        {
            InvoicesIssue.Clear();
            using (var context = new BochagovaProPracticeContext())
            {
                var invoices = context.IssuingInvoices
                    .Include(i => i.IdWarehouseNavigation)
                    .Include(i => i.IdDivisionNavigation)
                    .Include(i => i.IdCounterpartieRecivierNavigation)
                    .Include(i => i.IdEmployeeOnIssueNavigation)
                    .Include(i => i.IdInvoiceNavigation)
                    .ToList()
                    .Select(i => new IssuingInvoiceVM(i))
                    .ToList();

                foreach (var invoice in invoices)
                {
                    InvoicesIssue.Add(invoice);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WorkIssuingInvoice workIssuingInvoiceWindow = new WorkIssuingInvoice();
            bool? result = workIssuingInvoiceWindow.ShowDialog();
            if (result == true)
            {
                LoadInvoices();
            }
        }
    }
}
