using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class Division
{
    public int IdDivision { get; set; }

    public string NameDivision { get; set; } = null!;

    public int? IdEmployee { get; set; }

    public virtual ICollection<AccumulationRegister> AccumulationRegisters { get; set; } = new List<AccumulationRegister>();

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual ICollection<InvoiceCellMovement> InvoiceCellMovements { get; set; } = new List<InvoiceCellMovement>();

    public virtual ICollection<Invoice> InvoiceIdDivisionRecipientNavigations { get; set; } = new List<Invoice>();

    public virtual ICollection<Invoice> InvoiceIdDivisionSenderNavigations { get; set; } = new List<Invoice>();

    public virtual ICollection<IssuingInvoice> IssuingInvoices { get; set; } = new List<IssuingInvoice>();

    public virtual ICollection<LoadingInvoice> LoadingInvoices { get; set; } = new List<LoadingInvoice>();

    public virtual ICollection<UnloadingInvoice> UnloadingInvoices { get; set; } = new List<UnloadingInvoice>();

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
