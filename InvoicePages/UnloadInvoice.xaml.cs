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
    /// Логика взаимодействия для UnloadInvoice.xaml
    /// </summary>
    public partial class UnloadInvoice : Page
    {
        private BochagovaProPracticeContext _context;
        public ObservableCollection<UnloadInvoiceVM> InvoicesUnload { get; set; }

        public UnloadInvoice()
        {
            InitializeComponent();
            DataContext = this;
            InvoicesUnload = new ObservableCollection<UnloadInvoiceVM>();
            LoadInvoices();
            if (App.currentUser.IdRole != 2)
            {
                AddButton.Visibility = Visibility.Collapsed;
            }
        }
        
        private void LoadInvoices()
        {
            InvoicesUnload.Clear();
            using (var context = new BochagovaProPracticeContext())
            {
                var invoices = context.UnloadingInvoices
                    .Include(i => i.IdWarehouseNavigation)
                    .Include(i => i.IdDivisionNavigation)
                    .Include(i => i.IdCarNavigation)
                    .Include(i => i.IdEmployeeOnUnloadNavigation)
                    .Include(i => i.IdReasonDocLoadNavigation)
                    .ToList()
                    .Select(i => new UnloadInvoiceVM(i))
                    .ToList();

                foreach (var invoice in invoices)
                {
                    InvoicesUnload.Add(invoice);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WorkUnloadInvoice workUnloadInvoiceWindow = new WorkUnloadInvoice();
            bool? result = workUnloadInvoiceWindow.ShowDialog();
            if (result == true)
            {
                LoadInvoices();
            }
        }
    }
}
