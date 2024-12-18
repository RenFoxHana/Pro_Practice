using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class CarBrand
{
    public int IdCarBrand { get; set; }

    public string NameBrand { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
