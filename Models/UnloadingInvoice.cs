using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class UnloadingInvoice
{
    public DateTime Period { get; set; }

    public int IdUnload { get; set; }

    public int IdReasonDocLoad { get; set; }

    public int IdDivision { get; set; }

    public int IdWarehouse { get; set; }

    public int IdCar { get; set; }

    public int IdEmployeeOnUnload { get; set; }

    public virtual Car IdCarNavigation { get; set; } = null!;

    public virtual Division IdDivisionNavigation { get; set; } = null!;

    public virtual Employee IdEmployeeOnUnloadNavigation { get; set; } = null!;

    public virtual LoadingInvoice IdReasonDocLoadNavigation { get; set; } = null!;

    public virtual Warehouse IdWarehouseNavigation { get; set; } = null!;

    public virtual ICollection<UnloadingInvoicesDetail> UnloadingInvoicesDetails { get; set; } = new List<UnloadingInvoicesDetail>();
}
