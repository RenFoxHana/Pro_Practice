using Pro_Practice.Models;

namespace Pro_Practice.ViewModel
{
    public class InvoiceCellMovementsVM
    {
        public DateTime Period { get; set; }

        public int IdMovement { get; set; }

        public string Division { get; set; }

        public string Warehouse { get; set; }

        public InvoiceCellMovementsVM(InvoiceCellMovement cellMovement)
        {
            Period = cellMovement.Period;
            IdMovement = cellMovement.IdMovement;
            Division = cellMovement.IdDivisionNavigation.NameDivision;
            Warehouse = cellMovement.IdWarehouseNavigation.NameWarehouse;
        }

    }
}
