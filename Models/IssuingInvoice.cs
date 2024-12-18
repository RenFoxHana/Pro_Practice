namespace Pro_Practice.Models;

public partial class IssuingInvoice
{
    public DateTime? Period { get; set; }

    public int IdIssue { get; set; }

    public int IdInvoice { get; set; }

    public int IdDivision { get; set; }

    public int IdWarehouse { get; set; }

    public int IdEmployeeOnIssue { get; set; }

    public int IdCounterpartieRecivier { get; set; }

    public int NumberOfSeats { get; set; }

    public virtual Counterparty IdCounterpartieRecivierNavigation { get; set; } = null!;

    public virtual Division IdDivisionNavigation { get; set; } = null!;

    public virtual Employee IdEmployeeOnIssueNavigation { get; set; } = null!;

    public virtual Invoice IdInvoiceNavigation { get; set; } = null!;

    public virtual Warehouse IdWarehouseNavigation { get; set; } = null!;
}
