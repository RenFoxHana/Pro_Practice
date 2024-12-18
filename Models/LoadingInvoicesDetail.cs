namespace Pro_Practice.Models;

public partial class LoadingInvoicesDetail
{
    public int LineNumber { get; set; }

    public int IdLoad { get; set; }

    public int IdInvoice { get; set; }

    public int NumberOfSeats { get; set; }

    public virtual Invoice IdInvoiceNavigation { get; set; } = null!;

    public virtual LoadingInvoice IdLoadNavigation { get; set; } = null!;
}
