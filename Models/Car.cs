using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class Car
{
    public int IdCar { get; set; }

    public string NameCar { get; set; } = null!;

    public string LicensePlateNumber { get; set; } = null!;

    public int IdCarBrand { get; set; }

    public int IdEmployee { get; set; }

    public virtual ICollection<AccumulationRegister> AccumulationRegisters { get; set; } = new List<AccumulationRegister>();

    public virtual CarBrand IdCarBrandNavigation { get; set; } = null!;

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;

    public virtual ICollection<LoadingInvoice> LoadingInvoices { get; set; } = new List<LoadingInvoice>();

    public virtual ICollection<UnloadingInvoice> UnloadingInvoices { get; set; } = new List<UnloadingInvoice>();
}
