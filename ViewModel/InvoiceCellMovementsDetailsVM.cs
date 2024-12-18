using Pro_Practice.Models;
using System.ComponentModel;

namespace Pro_Practice.ViewModel
    {
    public class InvoiceCellMovementsDetailsVM
    {
        public int LineNumber { get; set; }

        public int IdMovement { get; set; }

        public int IdInvoice { get; set; }
        public string Invoice { get; set; }

        public string StorageCellFrom { get; set; }

        public string StorageCellTo { get; set; }

        public int NumberOfSeats { get; set; }

        public InvoiceCellMovementsDetailsVM(InvoiceCellMovementsDetail cellMovement)
        {
            LineNumber = cellMovement.LineNumber;
            IdMovement = cellMovement.IdMovement;
            IdInvoice = cellMovement.IdInvoice;
            Invoice = cellMovement.IdInvoiceNavigation.Period.ToString();
            StorageCellFrom = cellMovement.IdStorageCellFromNavigation.NameCell;
            StorageCellTo = cellMovement.IdStorageCellToNavigation.NameCell;
            NumberOfSeats = cellMovement.NumberOfSeats;
        }
    }   
}