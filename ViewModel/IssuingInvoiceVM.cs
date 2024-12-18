using Pro_Practice.Models;

namespace Pro_Practice.ViewModel
{
    public class IssuingInvoiceVM
    {
        public DateTime? Period { get; set; }

        public int IdIssue { get; set; }

        public int IdInvoice { get; set; }

        public string Invoice { get; set; }

        public string Division { get; set; }

        public string Warehouse { get; set; }

        public string EmployeeOnIssue { get; set; }

        public string CounterpartieRecivier { get; set; }

        public int NumberOfSeats { get; set; }

        public IssuingInvoiceVM(IssuingInvoice issuing)
        {
            Period = issuing.Period;
            IdIssue = issuing.IdIssue;
            IdInvoice = issuing.IdInvoice;
            Invoice = $"{issuing.IdInvoiceNavigation.Period} {issuing.IdInvoice}";
            Division = issuing.IdDivisionNavigation.NameDivision;
            Warehouse = issuing.IdWarehouseNavigation.NameWarehouse;
            EmployeeOnIssue = $"{issuing.IdEmployeeOnIssueNavigation.LastName} {issuing.IdEmployeeOnIssueNavigation.FirstName} {issuing?.IdEmployeeOnIssueNavigation.Patronymic}";
            CounterpartieRecivier = issuing.IdCounterpartieRecivierNavigation.NameCp;
            NumberOfSeats = issuing.NumberOfSeats;
        }

    }
}
