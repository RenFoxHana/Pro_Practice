using ClosedXML.Excel;
using System.Collections.ObjectModel;

namespace Pro_Practice.ViewModel
{
    public class ReportGenerator
    {
        public void GenerateReport(ObservableCollection<RegisterVM> registers, string filePath)
        {
            var filteredRegisters = FilterRegisters(registers);
            var aggregatedData = AggregateData(filteredRegisters);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Отчет");

                worksheet.Cell(1, 1).Value = "Подразделение";
                worksheet.Cell(1, 2).Value = "Склад";
                worksheet.Cell(1, 3).Value = "Количество мест накладной";
                worksheet.Cell(1, 4).Value = "Id накладной";
                worksheet.Cell(1, 5).Value = "Дата";
                worksheet.Cell(1, 6).Value = "Ячейка";  
                worksheet.Cell(1, 7).Value = "Машина";        
                worksheet.Cell(1, 8).Value = "Количество мест накладной";
                worksheet.Cell(1, 9).Value = "Id накладной";
                worksheet.Cell(1, 10).Value = "Дата";

                int row = 2;
                foreach (var item in aggregatedData.DivisionWarehouseData)
                {
                    worksheet.Cell(row, 1).Value = item.Division;
                    worksheet.Cell(row, 2).Value = item.Warehouse;
                    worksheet.Cell(row, 3).Value = item.NumberOfSeats;
                    worksheet.Cell(row, 4).Value = item.InvoiceId;   
                    worksheet.Cell(row, 5).Value = item.Period;      
                    worksheet.Cell(row, 6).Value = item.StorageCell; 

                    row++;
                }

                row = 2;
                foreach (var item in aggregatedData.CarData)
                {
                    worksheet.Cell(row, 7).Value = item.Car;
                    worksheet.Cell(row, 8).Value = item.NumberOfSeats;
                    worksheet.Cell(row, 9).Value = item.InvoiceId;
                    worksheet.Cell(row, 10).Value = item.Period;
                    row++;
                }

                worksheet.ColumnsUsed().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }

        private ObservableCollection<RegisterVM> FilterRegisters(ObservableCollection<RegisterVM> registers)
        {
            var filteredRegisters = new ObservableCollection<RegisterVM>();

            var invoicesWithMovement = registers
                .Where(r => r.TypeMovement == "Приход" || r.TypeMovement == "Расход")
                .GroupBy(r => r.InvoiceId)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var invoiceGroup in invoicesWithMovement)
            {
                var lastInvoiceEntry = invoiceGroup.Value
                    .Where(r => r.TypeMovement == "Приход")
                    .OrderByDescending(r => r.Period)
                    .FirstOrDefault();

                if (lastInvoiceEntry != null && !invoiceGroup.Value.Any(r => r.TypeMovement == "Расход" && r.DocumentType == "Выдача накладной клиенту"))
                {
                    filteredRegisters.Add(lastInvoiceEntry);
                }
            }

            return filteredRegisters;
        }

        private (List<DivisionWarehouseData> DivisionWarehouseData, List<CarData> CarData) AggregateData(ObservableCollection<RegisterVM> registers)
        {
            var divisionWarehouseData = registers
                .Where(r => !string.IsNullOrEmpty(r.Division) && !string.IsNullOrEmpty(r.Warehouse))
                .GroupBy(r => new { r.Division, r.Warehouse })
                .Select(g => new DivisionWarehouseData
                {
                    Division = g.Key.Division,
                    Warehouse = g.Key.Warehouse,
                    NumberOfSeats = g.Sum(r => r.NumberOfSeats),
                    InvoiceId = g.First().InvoiceId,
                    Period = g.First().Period,
                    StorageCell = g.First().StorageCell
                })
                .ToList();

            var carData = registers
                .Where(r => !string.IsNullOrEmpty(r.Car))
                .GroupBy(r => r.Car)
                .Select(g => new CarData
                {
                    Car = g.Key,
                    NumberOfSeats = g.Sum(r => r.NumberOfSeats),
                    InvoiceId = g.First().InvoiceId,
                    Period = g.First().Period,
                })
                .ToList();

            return (divisionWarehouseData, carData);
        }

        public class DivisionWarehouseData
        {
            public string Division { get; set; }
            public string Warehouse { get; set; }
            public int NumberOfSeats { get; set; }
            public int InvoiceId { get; set; }
            public DateTime Period { get; set; }
            public string StorageCell { get; set; }
        }

        public class CarData
        {
            public string Car { get; set; }
            public int NumberOfSeats { get; set; }
            public int InvoiceId { get; set; }
            public DateTime Period { get; set; }
        }
    }
}