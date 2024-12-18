using Pro_Practice.Models;
using Pro_Practice.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Pro_Practice.InvoicePages
{
    /// <summary>
    /// Логика взаимодействия для TypeOfCounterpartie.xaml
    /// </summary>
    public partial class TypeCounterpartie : Page
    {
        private BochagovaProPracticeContext _context = new BochagovaProPracticeContext();
        public ObservableCollection<TypesOfCounterparty> ListTypeCounterpartie { get; set; }
        public TypeCounterpartie()
        {
            InitializeComponent();
            TypeCounterpartieVM viewModel = new TypeCounterpartieVM();
            DataContext = viewModel;
            ListTypeCounterpartie = new ObservableCollection<TypesOfCounterparty>(_context.TypesOfCounterparties.ToList());
        }
    }
}
