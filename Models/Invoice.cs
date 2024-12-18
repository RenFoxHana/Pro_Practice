using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class Invoice
{
    public DateTime Period { get; set; }

    public int IdInvoice { get; set; }

    public decimal VolumeCargo { get; set; }

    public decimal WeightCargo { get; set; }

    public int NumberOfSeats { get; set; }

    public int IdTypeCargo { get; set; }

    public int IdCounterpartieSender { get; set; }

    public int IdSendersAddress { get; set; }

    public int IdCounterpartieRecipient { get; set; }

    public int IdRecipientsAddress { get; set; }

    public int IdDivisionSender { get; set; }

    public int IdWarehouseSender { get; set; }

    public int IdDivisionRecipient { get; set; }

    public int IdEmployeeAtAcceptance { get; set; }

    public virtual Counterparty IdCounterpartieRecipientNavigation { get; set; } = null!;

    public virtual Counterparty IdCounterpartieSenderNavigation { get; set; } = null!;

    public virtual Division IdDivisionRecipientNavigation { get; set; } = null!;

    public virtual Division IdDivisionSenderNavigation { get; set; } = null!;

    public virtual Employee IdEmployeeAtAcceptanceNavigation { get; set; } = null!;

    public virtual Address IdRecipientsAddressNavigation { get; set; } = null!;

    public virtual Address IdSendersAddressNavigation { get; set; } = null!;

    public virtual TypesOfCargo IdTypeCargoNavigation { get; set; } = null!;

    public virtual Warehouse IdWarehouseSenderNavigation { get; set; } = null!;

    public virtual ICollection<InvoiceCellMovementsDetail> InvoiceCellMovementsDetails { get; set; } = new List<InvoiceCellMovementsDetail>();

    public virtual ICollection<IssuingInvoice> IssuingInvoices { get; set; } = new List<IssuingInvoice>();

    public virtual ICollection<LoadingInvoicesDetail> LoadingInvoicesDetails { get; set; } = new List<LoadingInvoicesDetail>();

    public virtual ICollection<UnloadingInvoicesDetail> UnloadingInvoicesDetails { get; set; } = new List<UnloadingInvoicesDetail>();
}
