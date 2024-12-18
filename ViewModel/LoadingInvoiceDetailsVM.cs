using Pro_Practice.Models;

namespace Pro_Practice.ViewModel
{
    public class LoadingInvoiceDetailsVM
    {
        public int LineNumber { get; set; }

        public int IdLoad { get; set; }

        public int IdInvoice { get; set; }

        public int NumberOfSeats { get; set; }

        public LoadingInvoiceDetailsVM(LoadingInvoicesDetail loadingInvoiceDetails)
        {
            LineNumber = loadingInvoiceDetails.LineNumber;
            IdLoad = loadingInvoiceDetails.IdLoad;
            IdInvoice = loadingInvoiceDetails.IdInvoice;
            NumberOfSeats = loadingInvoiceDetails.NumberOfSeats;
        }
    }
}
