using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class InvoiceCellMovement
{
    public DateTime Period { get; set; }

    public int IdMovement { get; set; }

    public int IdDivision { get; set; }

    public int IdWarehouse { get; set; }

    public virtual Division IdDivisionNavigation { get; set; } = null!;

    public virtual Warehouse IdWarehouseNavigation { get; set; } = null!;

    public virtual ICollection<InvoiceCellMovementsDetail> InvoiceCellMovementsDetails { get; set; } = new List<InvoiceCellMovementsDetail>();
}
