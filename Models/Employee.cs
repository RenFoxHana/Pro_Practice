using System;
using System.Collections.Generic;

namespace Pro_Practice.Models;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int IdDivision { get; set; }

    public int? IdRole { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Division> Divisions { get; set; } = new List<Division>();

    public virtual Division IdDivisionNavigation { get; set; } = null!;

    public virtual UserRole? IdRoleNavigation { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<IssuingInvoice> IssuingInvoices { get; set; } = new List<IssuingInvoice>();

    public virtual ICollection<LoadingInvoice> LoadingInvoices { get; set; } = new List<LoadingInvoice>();

    public virtual ICollection<UnloadingInvoice> UnloadingInvoices { get; set; } = new List<UnloadingInvoice>();
}
