using System.ComponentModel;
using System.Runtime.CompilerServices;
using Pro_Practice.Models;

namespace Pro_Practice.ViewModel
{
    public class RegisterVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime _period;
        public DateTime Period
        {
            get { return _period; }
            set
            {
                if (_period != value)
                {
                    _period = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _documentType;
        public string DocumentType
        {
            get { return _documentType; }
            set
            {
                if (_documentType != value)
                {
                    _documentType = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _typeMovement;
        public string TypeMovement
        {
            get { return _typeMovement; }
            set
            {
                if (_typeMovement != value)
                {
                    _typeMovement = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _documentId;
        public int DocumentId
        {
            get { return _documentId; }
            set
            {
                if (_documentId != value)
                {
                    _documentId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _division;
        public string? Division
        {
            get { return _division; }
            set
            {
                if (_division != value)
                {
                    _division = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _warehouse;
        public string? Warehouse
        {
            get { return _warehouse; }
            set
            {
                if (_warehouse != value)
                {
                    _warehouse = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _storageCell;
        public string? StorageCell
        {
            get { return _storageCell; }
            set
            {
                if (_storageCell != value)
                {
                    _storageCell = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _car;
        public string? Car
        {
            get { return _car; }
            set
            {
                if (_car != value)
                {
                    _car = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _invoiceId;
        public int InvoiceId
        {
            get { return _invoiceId; }
            set
            {
                if (_invoiceId != value)
                {
                    _invoiceId = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _numberOfSeats;
        public int NumberOfSeats
        {
            get { return _numberOfSeats; }
            set
            {
                if (_numberOfSeats != value)
                {
                    _numberOfSeats = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _registrator;
        public string Registrator
        {
            get { return _registrator; }
            set
            {
                if (_registrator != value)
                {
                    _registrator = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RegisterVM(AccumulationRegister register)
        {
            Period = register.Period;
            TypeMovement = register.MovementType;
            DocumentType = register.IdDocTypeNavigation.NameOfType;
            DocumentId = register.IdDocument;
            Division = register.IdDivisionNavigation?.NameDivision;
            Warehouse = register.IdWarehouseNavigation?.NameWarehouse;
            StorageCell = register.IdStorageCellNavigation?.NameCell;
            Car = register.IdCarNavigation?.NameCar;
            InvoiceId = register.IdInvoice;
            NumberOfSeats = register.NumberOfSeats ?? 0;
            Registrator = $"{register.IdDocTypeNavigation.NameOfType} {register.IdDocument}";
        }
    }
}