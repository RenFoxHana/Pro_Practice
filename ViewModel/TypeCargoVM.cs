using Pro_Practice.Models;
using System.Collections.ObjectModel;

namespace Pro_Practice.ViewModel
{
    internal class TypeCargoVM
    {
        private BochagovaProPracticeContext _context = new BochagovaProPracticeContext();
        public ObservableCollection<TypesOfCargo> ListTypeCargo { get; set; }

        public TypeCargoVM()
        {
            ListTypeCargo = new ObservableCollection<TypesOfCargo>(_context.TypesOfCargos.ToList());
        }
    }
}
