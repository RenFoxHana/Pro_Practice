using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class Warehouse
{
    public int IdWarehouse { get; set; }

    public string NameWarehouse { get; set; } = null!;

    public int IdDivision { get; set; }

    public int? DefaultCell { get; set; }

    public virtual ICollection<AccumulationRegister> AccumulationRegisters { get; set; } = new List<AccumulationRegister>();

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual StorageCell? DefaultCellNavigation { get; set; }

    public virtual Division IdDivisionNavigation { get; set; } = null!;

    public virtual ICollection<InvoiceCellMovement> InvoiceCellMovements { get; set; } = new List<InvoiceCellMovement>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<IssuingInvoice> IssuingInvoices { get; set; } = new List<IssuingInvoice>();

    public virtual ICollection<LoadingInvoice> LoadingInvoices { get; set; } = new List<LoadingInvoice>();

    public virtual ICollection<StorageCell> StorageCells { get; set; } = new List<StorageCell>();

    public virtual ICollection<UnloadingInvoice> UnloadingInvoices { get; set; } = new List<UnloadingInvoice>();
}
