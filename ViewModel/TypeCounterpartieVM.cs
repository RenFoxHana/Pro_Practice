using Pro_Practice.Models;
using System.Collections.ObjectModel;

namespace Pro_Practice.ViewModel
{
    internal class TypeCounterpartieVM
    {
        private BochagovaProPracticeContext _context = new BochagovaProPracticeContext();
        public ObservableCollection<TypesOfCounterparty> ListTypeCounterpartie { get; set; }

        public TypeCounterpartieVM()
        {
            ListTypeCounterpartie = new ObservableCollection<TypesOfCounterparty>(_context.TypesOfCounterparties.ToList());
        }
    }
}
