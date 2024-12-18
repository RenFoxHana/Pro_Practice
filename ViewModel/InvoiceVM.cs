using Pro_Practice.Models;

namespace Pro_Practice.ViewModel
{
    public class InvoiceVM
    {
        public int IdInvoice { get; set; }
        public DateTime Period { get; set; }
        public decimal VolumeCargo { get; set; }
        public decimal WeightCargo { get; set; }
        public int NumberOfSeats { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string TypeCargoName { get; set; }
        public string DivisionSender { get; set; }
        public string WarehouseSender { get; set; }
        public string DivisionRecipient { get; set; }
        public string EmployeeAtAcceptance { get; set; }

        public InvoiceVM(Invoice invoice)
        {
            IdInvoice = invoice.IdInvoice;
            Period = invoice.Period;
            VolumeCargo = invoice.VolumeCargo;
            WeightCargo = invoice.WeightCargo;
            NumberOfSeats = invoice.NumberOfSeats;

            var senderCounterparty = invoice.IdCounterpartieSenderNavigation;
            var recipientCounterparty = invoice.IdCounterpartieRecipientNavigation;

            SenderName = senderCounterparty?.NameCp ?? "Не указан";
            SenderAddress = senderCounterparty?.Addresses
                                              .Select(a => new AddressVM(a))
                                              .FirstOrDefault()?.FullAddress ?? "Не указан";

            RecipientName = recipientCounterparty?.NameCp ?? "Не указан";
            RecipientAddress = recipientCounterparty?.Addresses
                                                   .Select(a => new AddressVM(a))
                                                   .FirstOrDefault()?.FullAddress ?? "Не указан";

            TypeCargoName = invoice.IdTypeCargoNavigation?.NameTypeCargo ?? "Не указан";
            DivisionSender = invoice.IdDivisionSenderNavigation?.NameDivision ?? "Не указан";
            WarehouseSender = invoice.IdWarehouseSenderNavigation?.NameWarehouse ?? "Не указан";
            DivisionRecipient = invoice.IdDivisionRecipientNavigation?.NameDivision ?? "Не указан";

            var employeeInSameDivision = invoice.IdDivisionSenderNavigation?.Employees
                                          .FirstOrDefault(e => e.IdDivision == invoice.IdDivisionSender);
            EmployeeAtAcceptance = employeeInSameDivision != null ?
                $"{employeeInSameDivision.FirstName} {employeeInSameDivision.LastName} {employeeInSameDivision.Patronymic}" :
                "Не указан";
        }
    }
}