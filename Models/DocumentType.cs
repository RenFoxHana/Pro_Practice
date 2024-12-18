using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class DocumentType
{
    public int IdDocType { get; set; }

    public string? NameOfType { get; set; }

    public virtual ICollection<AccumulationRegister> AccumulationRegisters { get; set; } = new List<AccumulationRegister>();
}
