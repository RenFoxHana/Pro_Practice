using Pro_Practice.Models;

namespace Pro_Practice.ViewModel
{
    public class UnloadInvoiceVM
    {
        public DateTime Period { get; set; }

        public int IdUnload { get; set; }

        public string ReasonDocLoad { get; set; }
        public int IdReasonDocLoad { get; set; }

        public string Division { get; set; }

        public string Warehouse { get; set; }

        public string Car { get; set; }

        public string EmployeeOnUnload { get; set; }

        public UnloadInvoiceVM(UnloadingInvoice unloadingInvoice)
        {
            Period = unloadingInvoice.Period;
            IdUnload = unloadingInvoice.IdUnload;
            ReasonDocLoad = $"{unloadingInvoice.IdReasonDocLoadNavigation.IdLoad} {unloadingInvoice.IdReasonDocLoadNavigation.Period}";
            IdReasonDocLoad = unloadingInvoice.IdReasonDocLoad;
            Division = unloadingInvoice.IdDivisionNavigation.NameDivision;
            Warehouse = unloadingInvoice.IdWarehouseNavigation.NameWarehouse;
            Car = unloadingInvoice.IdCarNavigation.NameCar;
            EmployeeOnUnload = $"{unloadingInvoice.IdEmployeeOnUnloadNavigation.LastName} {unloadingInvoice.IdEmployeeOnUnloadNavigation.FirstName} {unloadingInvoice?.IdEmployeeOnUnloadNavigation.Patronymic}";
        }
    }
}
