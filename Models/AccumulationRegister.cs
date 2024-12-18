namespace Pro_Practice.Models;

public partial class AccumulationRegister
{
    public int IdEntry { get; set; }

    public DateTime Period { get; set; }

    public int IdDocument { get; set; }

    public int IdDocType { get; set; }

    public int IdInvoice { get; set; }

    public int? NumberOfSeats { get; set; }

    public string MovementType { get; set; } = null!;

    public int? IdDivision { get; set; }

    public int? IdWarehouse { get; set; }

    public int? IdStorageCell { get; set; }

    public int? IdCar { get; set; }

    public virtual Car? IdCarNavigation { get; set; }

    public virtual Division? IdDivisionNavigation { get; set; }

    public virtual DocumentType IdDocTypeNavigation { get; set; } = null!;

    public virtual StorageCell? IdStorageCellNavigation { get; set; }

    public virtual Warehouse? IdWarehouseNavigation { get; set; }
}
