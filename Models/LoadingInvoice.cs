using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class LoadingInvoice
{
    public DateTime Period { get; set; }

    public int IdLoad { get; set; }

    public int IdDivision { get; set; }

    public int IdWarehouse { get; set; }

    public int IdCar { get; set; }

    public int IdEmployeeOnLoad { get; set; }

    public virtual Car IdCarNavigation { get; set; } = null!;

    public virtual Division IdDivisionNavigation { get; set; } = null!;

    public virtual Employee IdEmployeeOnLoadNavigation { get; set; } = null!;

    public virtual Warehouse IdWarehouseNavigation { get; set; } = null!;

    public virtual ICollection<LoadingInvoicesDetail> LoadingInvoicesDetails { get; set; } = new List<LoadingInvoicesDetail>();

    public virtual ICollection<UnloadingInvoice> UnloadingInvoices { get; set; } = new List<UnloadingInvoice>();
}
