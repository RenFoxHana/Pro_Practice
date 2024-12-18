using Pro_Practice.Models;

namespace Pro_Practice.ViewModel
{
    public class StorageCellVM
    {
        public int IdStorageCell { get; set; }

        public string NameCell { get; set; } = null!;

        public string NameWarehouse { get; set; }

        public StorageCellVM() { }

        public StorageCellVM(StorageCell cell)
        {
            IdStorageCell = cell.IdStorageCell;
            NameCell = cell.NameCell;
            NameWarehouse = cell.IdWarehouseNavigation.NameWarehouse;
        }
    }
}
