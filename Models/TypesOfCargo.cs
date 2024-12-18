using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class TypesOfCargo
{
    public int IdTypeCargo { get; set; }

    public string NameTypeCargo { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
