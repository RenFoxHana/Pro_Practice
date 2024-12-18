using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class Counterparty
{
    public int IdCounterpartie { get; set; }

    public int IdTypeCp { get; set; }

    public string NameCp { get; set; } = null!;

    public string? PassportData { get; set; }

    public string? TaxpayerIdentificationNumber { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual TypesOfCounterparty IdTypeCpNavigation { get; set; } = null!;

    public virtual ICollection<Invoice> InvoiceIdCounterpartieRecipientNavigations { get; set; } = new List<Invoice>();

    public virtual ICollection<Invoice> InvoiceIdCounterpartieSenderNavigations { get; set; } = new List<Invoice>();

    public virtual ICollection<IssuingInvoice> IssuingInvoices { get; set; } = new List<IssuingInvoice>();
}
