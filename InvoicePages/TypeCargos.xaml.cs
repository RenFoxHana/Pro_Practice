using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using Pro_Practice.WorkWithInvoice;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Pro_Practice.InvoicePages
{
    /// <summary>
    /// Логика взаимодействия для TypeCargo.xaml
    /// </summary>
    public partial class TypeCargos : Page
    {
        private BochagovaProPracticeContext _context = new BochagovaProPracticeContext();
        public ObservableCollection<TypesOfCargo> ListTypeCargos { get; set; }
        public TypeCargos()
        {
            InitializeComponent();
            TypeCargoVM viewModel = new TypeCargoVM();
            DataContext = viewModel;
            ListTypeCargos = new ObservableCollection<TypesOfCargo>(_context.TypesOfCargos.ToList());
            if (App.currentUser.IdRole != 2)
            {
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }

        private TypesOfCargo _selectedTypeCargo;
        public TypesOfCargo SelectedTypeCargo
        {
            get { return _selectedTypeCargo; }
            set
            {
                _selectedTypeCargo = value;
            }
        }

        private void ListViewTypeCargo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewTypeCargo.SelectedItem is TypesOfCargo selectedType)
            {
                SelectedTypeCargo = selectedType;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var workTypeCargoWindow = new WorkTypeCargo(new BochagovaProPracticeContext());
            workTypeCargoWindow.ShowDialog();
            Refresh();

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTypeCargo = SelectedTypeCargo;
            var workTypeCargoWindow = new WorkTypeCargo(new BochagovaProPracticeContext(), selectedTypeCargo);
            workTypeCargoWindow.ShowDialog();
            Refresh();

        }


        public void Refresh()
        {
            TypeCargoVM viewModel = (TypeCargoVM)DataContext;
            viewModel.ListTypeCargo.Clear();

            var typecargos = _context.TypesOfCargos.ToList();

            foreach (var typecargo in typecargos)
            {
                viewModel.ListTypeCargo.Add(typecargo);
            }
        }
    }
}
