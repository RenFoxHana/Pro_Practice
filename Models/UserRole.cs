using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class UserRole
{
    public int IdRole { get; set; }

    public string NameRole { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
