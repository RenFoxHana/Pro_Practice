using Pro_Practice.Models;

namespace Pro_Practice.ViewModel
{
    public class LoadingInvoiceVM
    {
        public DateTime Period { get; set; }

        public int IdLoad { get; set; }

        public string Division { get; set; }

        public string Warehouse { get; set; }

        public string Car { get; set; }

        public string EmployeeOnLoad { get; set; }

        public LoadingInvoiceVM(LoadingInvoice loadingInvoice)
        {
            Period = loadingInvoice.Period;
            IdLoad = loadingInvoice.IdLoad;
            Division = loadingInvoice.IdDivisionNavigation.NameDivision;
            Warehouse = loadingInvoice.IdWarehouseNavigation.NameWarehouse;
            Car = loadingInvoice.IdCarNavigation.NameCar;
            EmployeeOnLoad = $"{loadingInvoice.IdEmployeeOnLoadNavigation.LastName} {loadingInvoice.IdEmployeeOnLoadNavigation.FirstName} {loadingInvoice.IdEmployeeOnLoadNavigation?.Patronymic}";
        }
    }
}
