using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class StorageCell
{
    public int IdStorageCell { get; set; }

    public string NameCell { get; set; } = null!;

    public int IdWarehouse { get; set; }

    public virtual ICollection<AccumulationRegister> AccumulationRegisters { get; set; } = new List<AccumulationRegister>();

    public virtual Warehouse IdWarehouseNavigation { get; set; } = null!;

    public virtual ICollection<InvoiceCellMovementsDetail> InvoiceCellMovementsDetailIdStorageCellFromNavigations { get; set; } = new List<InvoiceCellMovementsDetail>();

    public virtual ICollection<InvoiceCellMovementsDetail> InvoiceCellMovementsDetailIdStorageCellToNavigations { get; set; } = new List<InvoiceCellMovementsDetail>();

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
