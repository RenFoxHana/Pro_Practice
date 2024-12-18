using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class InvoiceCellMovementsDetail
{
    public int LineNumber { get; set; }

    public int IdMovement { get; set; }

    public int IdInvoice { get; set; }

    public int IdStorageCellFrom { get; set; }

    public int IdStorageCellTo { get; set; }

    public int NumberOfSeats { get; set; }

    public virtual Invoice IdInvoiceNavigation { get; set; } = null!;

    public virtual InvoiceCellMovement IdMovementNavigation { get; set; } = null!;

    public virtual StorageCell IdStorageCellFromNavigation { get; set; } = null!;

    public virtual StorageCell IdStorageCellToNavigation { get; set; } = null!;
}
