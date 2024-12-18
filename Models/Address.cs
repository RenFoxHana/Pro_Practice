using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class Address
{
    public int IdAddress { get; set; }

    public string Latitude { get; set; } = null!;

    public string Longitude { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string District { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Locality { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string HouseOrLandPlot { get; set; } = null!;

    public string Room { get; set; } = null!;

    public int? IdCounterpartie { get; set; }

    public int? IdDivision { get; set; }

    public int? IdWarehouse { get; set; }

    public virtual Counterparty? IdCounterpartieNavigation { get; set; }

    public virtual Division? IdDivisionNavigation { get; set; }

    public virtual Warehouse? IdWarehouseNavigation { get; set; }

    public virtual ICollection<Invoice> InvoiceIdRecipientsAddressNavigations { get; set; } = new List<Invoice>();

    public virtual ICollection<Invoice> InvoiceIdSendersAddressNavigations { get; set; } = new List<Invoice>();
}
