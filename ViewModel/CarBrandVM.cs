using Pro_Practice.Models;
using System.Collections.ObjectModel;

namespace Pro_Practice.ViewModel
{
    internal class CarBrandVM
    {
        private BochagovaProPracticeContext _context = new BochagovaProPracticeContext();
        public ObservableCollection<CarBrand> ListCarBrands { get; set; }

        public CarBrandVM()
        {
            ListCarBrands = new ObservableCollection<CarBrand>(_context.CarBrands.ToList());
        }
    }
}
