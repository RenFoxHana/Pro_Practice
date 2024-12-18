using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class TypesOfCounterparty
{
    public int IdTypeCp { get; set; }

    public string NameType { get; set; } = null!;

    public virtual ICollection<Counterparty> Counterparties { get; set; } = new List<Counterparty>();
}
