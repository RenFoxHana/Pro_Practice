using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Pro_Practice.Models;

namespace Pro_Practice.ViewModel
{
    public class RegisterViewModel
    {
        public ObservableCollection<RegisterVM> Registers { get; set; }

        public RegisterViewModel()
        {
            Registers = new ObservableCollection<RegisterVM>();
            LoadRegisters();
        }

        private void LoadRegisters()
        {
            using (var context = new BochagovaProPracticeContext())
            {
                var registers = context.AccumulationRegisters
                    .Include(r => r.IdDocTypeNavigation)
                    .Include(r => r.IdDivisionNavigation)
                    .Include(r => r.IdWarehouseNavigation)
                    .Include(r => r.IdStorageCellNavigation)
                    .Include(r => r.IdCarNavigation)
                    .ToList();

                foreach (var register in registers)
                {
                    Registers.Add(new RegisterVM(register));
                }
            }
        }
    }
}