namespace Pro_Practice.Models;

public partial class UnloadingInvoicesDetail
{
    public int LineNumber { get; set; }

    public int IdUnload { get; set; }

    public int IdInvoice { get; set; }

    public int NumberOfSeats { get; set; }

    public virtual Invoice IdInvoiceNavigation { get; set; } = null!;

    public virtual UnloadingInvoice IdUnloadNavigation { get; set; } = null!;
}
