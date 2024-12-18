using Pro_Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pro_Practice.ViewModel
{
    public class AddressVM
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

        public string? NameCounterpatie { get; set; }

        public string? NameDivision { get; set; }

        public string? NameWarehouse { get; set; }

        public string FullAddress => $"{Region}, {District}, {City}, {Locality}, {Street}, {HouseOrLandPlot} {Room}";
        public AddressVM(Address addresses)
        {
            IdAddress = addresses.IdAddress;
            Latitude = addresses.Latitude;
            Longitude = addresses.Longitude;
            Region = addresses.Region;
            District = addresses.District;
            City = addresses.City;
            Locality = addresses.Locality;
            Street = addresses.Street;
            HouseOrLandPlot = addresses.HouseOrLandPlot;
            Room = addresses.Room;

            NameDivision = addresses.IdDivisionNavigation?.NameDivision ?? "Не указан";
            NameCounterpatie = addresses.IdCounterpartieNavigation?.NameCp ?? "Не указан";
            NameWarehouse = addresses.IdWarehouseNavigation?.NameWarehouse ?? "Не указан";
        }
    }
}
