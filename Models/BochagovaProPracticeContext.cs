using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pro_Practice.Models;

public partial class BochagovaProPracticeContext : DbContext
{
    public BochagovaProPracticeContext()
    {
    }

    public BochagovaProPracticeContext(DbContextOptions<BochagovaProPracticeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccumulationRegister> AccumulationRegisters { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarBrand> CarBrands { get; set; }

    public virtual DbSet<Counterparty> Counterparties { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceCellMovement> InvoiceCellMovements { get; set; }

    public virtual DbSet<InvoiceCellMovementsDetail> InvoiceCellMovementsDetails { get; set; }

    public virtual DbSet<IssuingInvoice> IssuingInvoices { get; set; }

    public virtual DbSet<LoadingInvoice> LoadingInvoices { get; set; }

    public virtual DbSet<LoadingInvoicesDetail> LoadingInvoicesDetails { get; set; }

    public virtual DbSet<StorageCell> StorageCells { get; set; }

    public virtual DbSet<TypesOfCargo> TypesOfCargos { get; set; }

    public virtual DbSet<TypesOfCounterparty> TypesOfCounterparties { get; set; }

    public virtual DbSet<UnloadingInvoice> UnloadingInvoices { get; set; }

    public virtual DbSet<UnloadingInvoicesDetail> UnloadingInvoicesDetails { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NATBOK\\MSSQLSERVER2;Initial Catalog=Bochagova_ProPractice;Integrated Security=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccumulationRegister>(entity =>
        {
            entity.HasKey(e => e.IdEntry);

            entity.ToTable("Accumulation_Register");

            entity.Property(e => e.IdEntry).HasColumnName("ID_Entry");
            entity.Property(e => e.IdCar).HasColumnName("ID_Car");
            entity.Property(e => e.IdDivision).HasColumnName("ID_Division");
            entity.Property(e => e.IdDocType).HasColumnName("ID_Doc_Type");
            entity.Property(e => e.IdDocument).HasColumnName("ID_Document");
            entity.Property(e => e.IdInvoice).HasColumnName("ID_Invoice");
            entity.Property(e => e.IdStorageCell).HasColumnName("ID_Storage_Cell");
            entity.Property(e => e.IdWarehouse).HasColumnName("ID_Warehouse");
            entity.Property(e => e.MovementType)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("Movement_Type");
            entity.Property(e => e.NumberOfSeats).HasColumnName("Number_Of_Seats");
            entity.Property(e => e.Period)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.AccumulationRegisters)
                .HasForeignKey(d => d.IdCar)
                .HasConstraintName("FK__Accumulat__ID_Ca__395884C4");

            entity.HasOne(d => d.IdDivisionNavigation).WithMany(p => p.AccumulationRegisters)
                .HasForeignKey(d => d.IdDivision)
                .HasConstraintName("FK_Accumulation_Register_Divisions");

            entity.HasOne(d => d.IdDocTypeNavigation).WithMany(p => p.AccumulationRegisters)
                .HasForeignKey(d => d.IdDocType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Accumulat__ID_Do__3587F3E0");

            entity.HasOne(d => d.IdStorageCellNavigation).WithMany(p => p.AccumulationRegisters)
                .HasForeignKey(d => d.IdStorageCell)
                .HasConstraintName("FK__Accumulat__ID_St__3864608B");

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.AccumulationRegisters)
                .HasForeignKey(d => d.IdWarehouse)
                .HasConstraintName("FK__Accumulat__ID_Wa__37703C52");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.IdAddress).HasName("PK__Addresse__73ED14D3F2803A5E");

            entity.Property(e => e.IdAddress).HasColumnName("ID_Address");
            entity.Property(e => e.City)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.District)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.HouseOrLandPlot)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("House_OR_land_plot");
            entity.Property(e => e.IdCounterpartie).HasColumnName("ID_Counterpartie");
            entity.Property(e => e.IdDivision).HasColumnName("ID_Division");
            entity.Property(e => e.IdWarehouse).HasColumnName("ID_Warehouse");
            entity.Property(e => e.Latitude)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Locality)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Longitude)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Region)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Room)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCounterpartieNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.IdCounterpartie)
                .HasConstraintName("FK__Addresses__ID_Co__534D60F1");

            entity.HasOne(d => d.IdDivisionNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.IdDivision)
                .HasConstraintName("FK__Addresses__ID_Di__5441852A");

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.IdWarehouse)
                .HasConstraintName("FK__Addresses__ID_Wa__5535A963");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.IdCar).HasName("PK__Cars__2BF9F616CAAC83C4");

            entity.Property(e => e.IdCar).HasColumnName("ID_Car");
            entity.Property(e => e.IdCarBrand).HasColumnName("ID_Car_Brand");
            entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");
            entity.Property(e => e.LicensePlateNumber)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("License_plate_number");
            entity.Property(e => e.NameCar)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCarBrandNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.IdCarBrand)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cars__ID_Car_Bra__33D4B598");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Cars)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cars__ID_Employe__34C8D9D1");
        });

        modelBuilder.Entity<CarBrand>(entity =>
        {
            entity.HasKey(e => e.IdCarBrand).HasName("PK__Car_Bran__9BEEEC48CCD4AB70");

            entity.ToTable("Car_Brands");

            entity.Property(e => e.IdCarBrand).HasColumnName("ID_Car_Brand");
            entity.Property(e => e.NameBrand)
                .HasMaxLength(70)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Counterparty>(entity =>
        {
            entity.HasKey(e => e.IdCounterpartie).HasName("PK__Counterp__89B6AEDD56593F33");

            entity.Property(e => e.IdCounterpartie).HasColumnName("ID_Counterpartie");
            entity.Property(e => e.IdTypeCp).HasColumnName("ID_Type_CP");
            entity.Property(e => e.NameCp)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("NameCP");
            entity.Property(e => e.PassportData)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Passport_Data");
            entity.Property(e => e.TaxpayerIdentificationNumber)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("Taxpayer_identification_number");

            entity.HasOne(d => d.IdTypeCpNavigation).WithMany(p => p.Counterparties)
                .HasForeignKey(d => d.IdTypeCp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Counterpa__ID_Ty__4E88ABD4");
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity.HasKey(e => e.IdDivision).HasName("PK__Division__9C55F1C0E6E2FAEC");

            entity.Property(e => e.IdDivision).HasColumnName("ID_Division");
            entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");
            entity.Property(e => e.NameDivision)
                .HasMaxLength(70)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Divisions)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK_Division_Employees");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.IdDocType).HasName("PK__Document__2310B15164473912");

            entity.ToTable("Document_Type");

            entity.Property(e => e.IdDocType).HasColumnName("ID_Doc_Type");
            entity.Property(e => e.NameOfType)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("Name_Of_Type");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__Employee__D9EE4F36E3983310");

            entity.Property(e => e.IdEmployee).HasColumnName("ID_Employee");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("First_Name");
            entity.Property(e => e.IdDivision).HasColumnName("ID_Division");
            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Last_Name");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Patronymic)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Phone_Number");

            entity.HasOne(d => d.IdDivisionNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdDivision)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__ID_Di__2D27B809");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_Employees_Users");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.IdInvoice).HasName("PK__Invoices__0540CA601F56C2A2");

            entity.ToTable(tb => tb.HasTrigger("AfterInvoiceInsert"));

            entity.Property(e => e.IdInvoice).HasColumnName("ID_Invoice");
            entity.Property(e => e.IdCounterpartieRecipient).HasColumnName("ID_Counterpartie_Recipient");
            entity.Property(e => e.IdCounterpartieSender).HasColumnName("ID_Counterpartie_Sender");
            entity.Property(e => e.IdDivisionRecipient).HasColumnName("ID_Division_Recipient");
            entity.Property(e => e.IdDivisionSender).HasColumnName("ID_Division_Sender");
            entity.Property(e => e.IdEmployeeAtAcceptance).HasColumnName("ID_Employee_At_Acceptance");
            entity.Property(e => e.IdRecipientsAddress).HasColumnName("ID_Recipients_Address");
            entity.Property(e => e.IdSendersAddress).HasColumnName("ID_Senders_Address");
            entity.Property(e => e.IdTypeCargo).HasColumnName("ID_Type_Cargo");
            entity.Property(e => e.IdWarehouseSender).HasColumnName("ID_Warehouse_Sender");
            entity.Property(e => e.NumberOfSeats).HasColumnName("Number_of_seats");
            entity.Property(e => e.Period)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VolumeCargo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.WeightCargo).HasColumnType("decimal(13, 2)");

            entity.HasOne(d => d.IdCounterpartieRecipientNavigation).WithMany(p => p.InvoiceIdCounterpartieRecipientNavigations)
                .HasForeignKey(d => d.IdCounterpartieRecipient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__ID_Cou__5AEE82B9");

            entity.HasOne(d => d.IdCounterpartieSenderNavigation).WithMany(p => p.InvoiceIdCounterpartieSenderNavigations)
                .HasForeignKey(d => d.IdCounterpartieSender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__ID_Cou__59063A47");

            entity.HasOne(d => d.IdDivisionRecipientNavigation).WithMany(p => p.InvoiceIdDivisionRecipientNavigations)
                .HasForeignKey(d => d.IdDivisionRecipient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__ID_Div__5EBF139D");

            entity.HasOne(d => d.IdDivisionSenderNavigation).WithMany(p => p.InvoiceIdDivisionSenderNavigations)
                .HasForeignKey(d => d.IdDivisionSender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__ID_Div__5CD6CB2B");

            entity.HasOne(d => d.IdEmployeeAtAcceptanceNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.IdEmployeeAtAcceptance)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__ID_Emp__5FB337D6");

            entity.HasOne(d => d.IdRecipientsAddressNavigation).WithMany(p => p.InvoiceIdRecipientsAddressNavigations)
                .HasForeignKey(d => d.IdRecipientsAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__ID_Rec__5BE2A6F2");

            entity.HasOne(d => d.IdSendersAddressNavigation).WithMany(p => p.InvoiceIdSendersAddressNavigations)
                .HasForeignKey(d => d.IdSendersAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__ID_Sen__59FA5E80");

            entity.HasOne(d => d.IdTypeCargoNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.IdTypeCargo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__ID_Typ__5812160E");

            entity.HasOne(d => d.IdWarehouseSenderNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.IdWarehouseSender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__ID_War__5DCAEF64");
        });

        modelBuilder.Entity<InvoiceCellMovement>(entity =>
        {
            entity.HasKey(e => e.IdMovement).HasName("PK__Invoice___7D835EE5ADC4ECAD");

            entity.ToTable("Invoice_Cell_Movements");

            entity.Property(e => e.IdMovement).HasColumnName("ID_Movement");
            entity.Property(e => e.IdDivision).HasColumnName("ID_Division");
            entity.Property(e => e.IdWarehouse).HasColumnName("ID_Warehouse");
            entity.Property(e => e.Period)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdDivisionNavigation).WithMany(p => p.InvoiceCellMovements)
                .HasForeignKey(d => d.IdDivision)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice_C__ID_Di__3D2915A8");

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.InvoiceCellMovements)
                .HasForeignKey(d => d.IdWarehouse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice_C__ID_Wa__3E1D39E1");
        });

        modelBuilder.Entity<InvoiceCellMovementsDetail>(entity =>
        {
            entity.HasKey(e => new { e.LineNumber, e.IdMovement }).HasName("PK__Invoice___2894E8BE9ACD33D0");

            entity.ToTable("Invoice_Cell_Movements_Details", tb => tb.HasTrigger("AfterInvoiceCellMovementsDetailsInsert"));

            entity.Property(e => e.LineNumber).HasColumnName("Line_Number");
            entity.Property(e => e.IdMovement).HasColumnName("ID_Movement");
            entity.Property(e => e.IdInvoice).HasColumnName("ID_Invoice");
            entity.Property(e => e.IdStorageCellFrom).HasColumnName("ID_Storage_Cell_From");
            entity.Property(e => e.IdStorageCellTo).HasColumnName("ID_Storage_Cell_To");
            entity.Property(e => e.NumberOfSeats).HasColumnName("Number_Of_Seats");

            entity.HasOne(d => d.IdInvoiceNavigation).WithMany(p => p.InvoiceCellMovementsDetails)
                .HasForeignKey(d => d.IdInvoice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice_C__ID_In__41EDCAC5");

            entity.HasOne(d => d.IdMovementNavigation).WithMany(p => p.InvoiceCellMovementsDetails)
                .HasForeignKey(d => d.IdMovement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice_C__ID_Mo__40F9A68C");

            entity.HasOne(d => d.IdStorageCellFromNavigation).WithMany(p => p.InvoiceCellMovementsDetailIdStorageCellFromNavigations)
                .HasForeignKey(d => d.IdStorageCellFrom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice_C__ID_St__42E1EEFE");

            entity.HasOne(d => d.IdStorageCellToNavigation).WithMany(p => p.InvoiceCellMovementsDetailIdStorageCellToNavigations)
                .HasForeignKey(d => d.IdStorageCellTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice_C__ID_St__43D61337");
        });

        modelBuilder.Entity<IssuingInvoice>(entity =>
        {
            entity.HasKey(e => e.IdIssue).HasName("PK__Issuing___1B925E28D2A2A69D");

            entity.ToTable("Issuing_Invoices", tb => tb.HasTrigger("AfterIssuingInvoicesInsert"));

            entity.Property(e => e.IdIssue).HasColumnName("ID_Issue");
            entity.Property(e => e.IdCounterpartieRecivier).HasColumnName("ID_Counterpartie_Recivier");
            entity.Property(e => e.IdDivision).HasColumnName("ID_Division");
            entity.Property(e => e.IdEmployeeOnIssue).HasColumnName("ID_Employee_On_Issue");
            entity.Property(e => e.IdInvoice).HasColumnName("ID_Invoice");
            entity.Property(e => e.IdWarehouse).HasColumnName("ID_Warehouse");
            entity.Property(e => e.NumberOfSeats).HasColumnName("Number_Of_Seats");
            entity.Property(e => e.Period).HasColumnType("datetime");

            entity.HasOne(d => d.IdCounterpartieRecivierNavigation).WithMany(p => p.IssuingInvoices)
                .HasForeignKey(d => d.IdCounterpartieRecivier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Issuing_I__ID_Co__607251E5");

            entity.HasOne(d => d.IdDivisionNavigation).WithMany(p => p.IssuingInvoices)
                .HasForeignKey(d => d.IdDivision)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Issuing_I__ID_Di__5D95E53A");

            entity.HasOne(d => d.IdEmployeeOnIssueNavigation).WithMany(p => p.IssuingInvoices)
                .HasForeignKey(d => d.IdEmployeeOnIssue)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Issuing_I__ID_Em__5F7E2DAC");

            entity.HasOne(d => d.IdInvoiceNavigation).WithMany(p => p.IssuingInvoices)
                .HasForeignKey(d => d.IdInvoice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Issuing_I__ID_In__5CA1C101");

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.IssuingInvoices)
                .HasForeignKey(d => d.IdWarehouse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Issuing_I__ID_Wa__5E8A0973");
        });

        modelBuilder.Entity<LoadingInvoice>(entity =>
        {
            entity.HasKey(e => e.IdLoad).HasName("PK__Loading___914C23114E37FB2A");

            entity.ToTable("Loading_Invoices");

            entity.Property(e => e.IdLoad).HasColumnName("ID_Load");
            entity.Property(e => e.IdCar).HasColumnName("ID_Car");
            entity.Property(e => e.IdDivision).HasColumnName("ID_Division");
            entity.Property(e => e.IdEmployeeOnLoad).HasColumnName("ID_Employee_On_Load");
            entity.Property(e => e.IdWarehouse).HasColumnName("ID_Warehouse");
            entity.Property(e => e.Period)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.LoadingInvoices)
                .HasForeignKey(d => d.IdCar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loading_I__ID_Ca__498EEC8D");

            entity.HasOne(d => d.IdDivisionNavigation).WithMany(p => p.LoadingInvoices)
                .HasForeignKey(d => d.IdDivision)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loading_I__ID_Di__47A6A41B");

            entity.HasOne(d => d.IdEmployeeOnLoadNavigation).WithMany(p => p.LoadingInvoices)
                .HasForeignKey(d => d.IdEmployeeOnLoad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loading_I__ID_Em__4A8310C6");

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.LoadingInvoices)
                .HasForeignKey(d => d.IdWarehouse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loading_I__ID_Wa__489AC854");
        });

        modelBuilder.Entity<LoadingInvoicesDetail>(entity =>
        {
            entity.HasKey(e => new { e.LineNumber, e.IdLoad }).HasName("PK__Loading___66581F6150303D8B");

            entity.ToTable("Loading_Invoices_Details", tb => tb.HasTrigger("AfterLoadingInvoicesDetailsInsert"));

            entity.Property(e => e.LineNumber).HasColumnName("Line_Number");
            entity.Property(e => e.IdLoad).HasColumnName("ID_Load");
            entity.Property(e => e.IdInvoice).HasColumnName("ID_Invoice");
            entity.Property(e => e.NumberOfSeats).HasColumnName("Number_Of_Seats");

            entity.HasOne(d => d.IdInvoiceNavigation).WithMany(p => p.LoadingInvoicesDetails)
                .HasForeignKey(d => d.IdInvoice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loading_I__ID_In__4E53A1AA");

            entity.HasOne(d => d.IdLoadNavigation).WithMany(p => p.LoadingInvoicesDetails)
                .HasForeignKey(d => d.IdLoad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loading_I__ID_Lo__4D5F7D71");
        });

        modelBuilder.Entity<StorageCell>(entity =>
        {
            entity.HasKey(e => e.IdStorageCell).HasName("PK__Storage___F7D7FE7EBCCBE044");

            entity.ToTable("Storage_Cells");

            entity.Property(e => e.IdStorageCell).HasColumnName("ID_Storage_Cell");
            entity.Property(e => e.IdWarehouse).HasColumnName("ID_Warehouse");
            entity.Property(e => e.NameCell)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.StorageCells)
                .HasForeignKey(d => d.IdWarehouse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Storage_C__ID_Wa__3A81B327");
        });

        modelBuilder.Entity<TypesOfCargo>(entity =>
        {
            entity.HasKey(e => e.IdTypeCargo).HasName("PK__Types_of__DA94F75310276737");

            entity.ToTable("Types_of_Cargo");

            entity.Property(e => e.IdTypeCargo).HasColumnName("ID_Type_Cargo");
            entity.Property(e => e.NameTypeCargo)
                .HasMaxLength(70)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TypesOfCounterparty>(entity =>
        {
            entity.HasKey(e => e.IdTypeCp).HasName("PK__Types_Of__D792CDAC0DC9842D");

            entity.ToTable("Types_Of_Counterparties");

            entity.Property(e => e.IdTypeCp).HasColumnName("ID_Type_CP");
            entity.Property(e => e.NameType)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UnloadingInvoice>(entity =>
        {
            entity.HasKey(e => e.IdUnload).HasName("PK__Unloadin__76EA2C9A55ECFE96");

            entity.ToTable("Unloading_Invoices");

            entity.Property(e => e.IdUnload).HasColumnName("ID_Unload");
            entity.Property(e => e.IdCar).HasColumnName("ID_Car");
            entity.Property(e => e.IdDivision).HasColumnName("ID_Division");
            entity.Property(e => e.IdEmployeeOnUnload).HasColumnName("ID_Employee_On_Unload");
            entity.Property(e => e.IdReasonDocLoad).HasColumnName("ID_Reason_Doc_Load");
            entity.Property(e => e.IdWarehouse).HasColumnName("ID_Warehouse");
            entity.Property(e => e.Period)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.UnloadingInvoices)
                .HasForeignKey(d => d.IdCar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Unloading__ID_Ca__55009F39");

            entity.HasOne(d => d.IdDivisionNavigation).WithMany(p => p.UnloadingInvoices)
                .HasForeignKey(d => d.IdDivision)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Unloading__ID_Di__531856C7");

            entity.HasOne(d => d.IdEmployeeOnUnloadNavigation).WithMany(p => p.UnloadingInvoices)
                .HasForeignKey(d => d.IdEmployeeOnUnload)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Unloading__ID_Em__55F4C372");

            entity.HasOne(d => d.IdReasonDocLoadNavigation).WithMany(p => p.UnloadingInvoices)
                .HasForeignKey(d => d.IdReasonDocLoad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Unloading__ID_Re__5224328E");

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.UnloadingInvoices)
                .HasForeignKey(d => d.IdWarehouse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Unloading__ID_Wa__540C7B00");
        });

        modelBuilder.Entity<UnloadingInvoicesDetail>(entity =>
        {
            entity.HasKey(e => new { e.LineNumber, e.IdUnload }).HasName("PK__Unloadin__D8227F9972887123");

            entity.ToTable("Unloading_Invoices_Details", tb => tb.HasTrigger("AfterUnloadingInvoicesDetailsInsert"));

            entity.Property(e => e.LineNumber).HasColumnName("Line_Number");
            entity.Property(e => e.IdUnload).HasColumnName("ID_Unload");
            entity.Property(e => e.IdInvoice).HasColumnName("ID_Invoice");
            entity.Property(e => e.NumberOfSeats).HasColumnName("Number_Of_Seats");

            entity.HasOne(d => d.IdInvoiceNavigation).WithMany(p => p.UnloadingInvoicesDetails)
                .HasForeignKey(d => d.IdInvoice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Unloading__ID_In__59C55456");

            entity.HasOne(d => d.IdUnloadNavigation).WithMany(p => p.UnloadingInvoicesDetails)
                .HasForeignKey(d => d.IdUnload)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Unloading__ID_Un__58D1301D");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.IdRole);

            entity.ToTable("User_Role");

            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.NameRole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_Role");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.IdWarehouse).HasName("PK__Warehous__6D0FABD20BF6F52E");

            entity.Property(e => e.IdWarehouse).HasColumnName("ID_Warehouse");
            entity.Property(e => e.DefaultCell).HasColumnName("Default_Cell");
            entity.Property(e => e.IdDivision).HasColumnName("ID_Division");
            entity.Property(e => e.NameWarehouse)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.DefaultCellNavigation).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.DefaultCell)
                .HasConstraintName("FK_Warehouses_Storage_Cells");

            entity.HasOne(d => d.IdDivisionNavigation).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.IdDivision)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Warehouse__ID_Di__37A5467C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
