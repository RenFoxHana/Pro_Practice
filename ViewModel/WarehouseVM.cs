using Pro_Practice.Models;

namespace Pro_Practice.ViewModel
{
    public class WarehouseVM
    {
        public int Id { get; set; }
        public string NameWarehouse { get; set; }
        public string Division { get; set; }
        public string? DefaultCell { get; set; }
        public string FullAddress { get; set; }

        public WarehouseVM() { }

        public WarehouseVM(Warehouse warehouse)
        {
            Id = warehouse.IdWarehouse;
            NameWarehouse = warehouse.NameWarehouse;
            Division = warehouse.IdDivisionNavigation.NameDivision;
            DefaultCell = warehouse.DefaultCellNavigation?.NameCell ?? "Не указана";

            // Формируем полный адрес
            var address = warehouse.Addresses?.FirstOrDefault();
            if (address != null)
            {
                FullAddress = $"{address.Region}, {address.District}, {address.City}, {address.Locality}, {address.Street}, {address.HouseOrLandPlot}";
                if (!string.IsNullOrEmpty(address.Room))
                {
                    FullAddress += $", Помещение: {address.Room}";
                }
            }
            else
            {
                FullAddress = "Адрес не указан";
            }
        }
    }
}