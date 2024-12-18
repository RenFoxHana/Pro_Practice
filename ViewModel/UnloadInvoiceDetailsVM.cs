using Pro_Practice.Models;

namespace Pro_Practice.ViewModel
{
    public class UnloadInvoiceDetailsVM
    {
        public int LineNumber { get; set; }

        public int IdUnload { get; set; }

        public int IdInvoice { get; set; }

        public int NumberOfSeats { get; set; }

        public UnloadInvoiceDetailsVM(UnloadingInvoicesDetail loadingDetail)
        {
            LineNumber = loadingDetail.LineNumber;
            IdUnload = loadingDetail.IdUnload;
            IdInvoice = loadingDetail.IdInvoice;
            NumberOfSeats = loadingDetail.NumberOfSeats;
        }
    }
}
