using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Pro_Practice.InvoicePages
{
    /// <summary>
    /// Логика взаимодействия для CargoInCells.xaml
    /// </summary>
    public partial class CargoInCells : Page
    {
        public ObservableCollection<RegisterVM> DataList { get; set; }
        public CargoInCells()
        {
            InitializeComponent();
            DataContext = this;
            DataList = new ObservableCollection<RegisterVM>();
            LoadData();

        }
        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            var reportGenerator = new ReportGenerator();

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                Title = "Сохранить отчет как"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                reportGenerator.GenerateReport(DataList, saveFileDialog.FileName);
            }
        }

        public void LoadData()
        {
            DataList.Clear();

            using (var context = new BochagovaProPracticeContext())
            {
                var invoices = context.AccumulationRegisters
                    .Include(i => i.IdDocTypeNavigation)
                    .Include(i => i.IdDivisionNavigation)
                    .Include(i => i.IdWarehouseNavigation)
                    .Include(i => i.IdStorageCellNavigation)
                    .Include(i => i.IdCarNavigation)
                    .ToList()
                    .Select(i => new RegisterVM(i))
                    .ToList();

                foreach (var invoice in invoices)
                {
                    DataList.Add(invoice);
                }
            }
        }
    }
}
