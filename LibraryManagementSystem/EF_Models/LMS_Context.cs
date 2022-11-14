using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LMS_Context : DbContext
    {
        public LMS_Context()
        {
        }

        public LMS_Context(DbContextOptions<LMS_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<LmsBookGenre> LmsBookGenres { get; set; } = null!;
        public virtual DbSet<LmsDefaultSystemSetting> LmsDefaultSystemSettings { get; set; } = null!;
        public virtual DbSet<LmsFineCalculation> LmsFineCalculations { get; set; } = null!;
        public virtual DbSet<LmsInventory> LmsInventories { get; set; } = null!;
        public virtual DbSet<LmsInventoryCode> LmsInventoryCodes { get; set; } = null!;
        public virtual DbSet<LmsInventoryEntryType> LmsInventoryEntryTypes { get; set; } = null!;
        public virtual DbSet<LmsInventoryHistory> LmsInventoryHistories { get; set; } = null!;
        public virtual DbSet<LmsInventoryRequest> LmsInventoryRequests { get; set; } = null!;
        public virtual DbSet<LmsInventoryRequestStatus> LmsInventoryRequestStatuses { get; set; } = null!;
        public virtual DbSet<LmsStaff> LmsStaffs { get; set; } = null!;
        public virtual DbSet<LmsStudent> LmsStudents { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-OIETVLB\\SHAZZ;Initial Catalog=LMS_DB;persist security info=True;User id=sa; Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LmsBookGenre>(entity =>
            {
                entity.HasKey(e => e.BookGenreId);

                entity.ToTable("LMS_BookGenre");
            });

            modelBuilder.Entity<LmsDefaultSystemSetting>(entity =>
            {
                entity.ToTable("LMS_DefaultSystemSetting");

                entity.Property(e => e.FinePerDay).HasColumnType("decimal(6, 2)");
            });

            modelBuilder.Entity<LmsFineCalculation>(entity =>
            {
                entity.HasKey(e => e.DueFineId);

                entity.ToTable("LMS_FineCalculation");

                entity.HasOne(d => d.InventoryRequestHistory)
                    .WithMany(p => p.LmsFineCalculations)
                    .HasForeignKey(d => d.InventoryRequestHistoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LMS_FineCalculation_LMS_InventoryHistory");
            });

            modelBuilder.Entity<LmsInventory>(entity =>
            {
                entity.ToTable("LMS_Inventory");

                entity.Property(e => e.BookAuthor).HasMaxLength(50);

                entity.Property(e => e.BookTitle).HasMaxLength(50);

                entity.Property(e => e.BookVersion).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.BookCodeNavigation)
                    .WithMany(p => p.LmsInventories)
                    .HasForeignKey(d => d.BookCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LMS_Inventory_LMS_InventoryCode");

                entity.HasOne(d => d.BookGenre)
                    .WithMany(p => p.LmsInventories)
                    .HasForeignKey(d => d.BookGenreId)
                    .HasConstraintName("FK_LMS_Inventory_LMS_Inventory");
            });

            modelBuilder.Entity<LmsInventoryCode>(entity =>
            {
                entity.ToTable("LMS_InventoryCode");

                entity.Property(e => e.Code).HasMaxLength(5);
            });

            modelBuilder.Entity<LmsInventoryEntryType>(entity =>
            {
                entity.HasKey(e => e.EntryTypeId);

                entity.ToTable("LMS_InventoryEntryType");

                entity.Property(e => e.EntryTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<LmsInventoryHistory>(entity =>
            {
                entity.ToTable("LMS_InventoryHistory");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.EntryType)
                    .WithMany(p => p.LmsInventoryHistories)
                    .HasForeignKey(d => d.EntryTypeId)
                    .HasConstraintName("FK_LMS_InventoryHistory_LMS_Inventory");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.LmsInventoryHistories)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LMS_InventoryHistory_LMS_Inventory1");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.LmsInventoryHistories)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LMS_InventoryHistory_LMS_Staff");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.LmsInventoryHistories)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LMS_InventoryHistory_LMS_Student");
            });

            modelBuilder.Entity<LmsInventoryRequest>(entity =>
            {
                entity.ToTable("Lms_InventoryRequest");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.LmsInventoryRequests)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lms_InventoryRequest_LMS_Inventory1");

                entity.HasOne(d => d.RequestStatus)
                    .WithMany(p => p.LmsInventoryRequests)
                    .HasForeignKey(d => d.RequestStatusId)
                    .HasConstraintName("FK_Lms_InventoryRequest_LMS_Inventory");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.LmsInventoryRequests)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_Lms_InventoryRequest_LMS_Staff");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.LmsInventoryRequests)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lms_InventoryRequest_LMS_Student");
            });

            modelBuilder.Entity<LmsInventoryRequestStatus>(entity =>
            {
                entity.HasKey(e => e.RequestStatusId)
                    .HasName("PK_InventoryRequestStatus");

                entity.ToTable("LMS_InventoryRequestStatus");

                entity.Property(e => e.RequestStatusDescription)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<LmsStaff>(entity =>
            {
                entity.ToTable("LMS_Staff");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            modelBuilder.Entity<LmsStudent>(entity =>
            {
                entity.ToTable("LMS_Student");

                entity.Property(e => e.Batch).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DueFine).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.RollNo).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
