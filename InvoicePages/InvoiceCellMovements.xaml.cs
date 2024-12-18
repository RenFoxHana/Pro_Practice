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
    /// Логика взаимодействия для InvoiceCellMovements.xaml
    /// </summary>
    public partial class InvoiceCellMovements : Page
    {
        private BochagovaProPracticeContext _context;
        public ObservableCollection<InvoiceCellMovementsVM> InvoicesCellList { get; set; }

        public InvoiceCellMovements()
        {
            InitializeComponent();
            DataContext = this;
            InvoicesCellList = new ObservableCollection<InvoiceCellMovementsVM>();
            LoadInvoicesMovement();
            if (App.currentUser.IdRole != 2)
            {
                AddButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadInvoicesMovement()
        {
            InvoicesCellList.Clear();
            using (var context = new BochagovaProPracticeContext())
            {
                var invoices = context.InvoiceCellMovements
                    .Include(i => i.IdWarehouseNavigation)
                    .Include(i => i.IdDivisionNavigation)
                    .ToList()
                    .Select(i => new InvoiceCellMovementsVM(i))
                    .ToList();

                foreach (var invoice in invoices)
                {
                    InvoicesCellList.Add(invoice);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WorkMovementCellInvoice workInvoiceMovementWindow = new WorkMovementCellInvoice();
            bool? result = workInvoiceMovementWindow.ShowDialog();
            if (result == true)
            {
                LoadInvoicesMovement();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //var selectedInvoiceMovemvent = (InvoiceCellMovementsVM)InvoiceMovementListView.SelectedItem;
            //if (selectedInvoiceMovemvent != null)
            //{
            //    WorkMovementCellInvoice workInvoiceMovementWindow = new WorkMovementCellInvoice(selectedInvoiceMovemvent);
            //    bool? result = workInvoiceMovementWindow.ShowDialog();
            //    if (result == true)
            //    {
            //        LoadInvoicesMovement();
            //    }
            //}
        }
    }
}
