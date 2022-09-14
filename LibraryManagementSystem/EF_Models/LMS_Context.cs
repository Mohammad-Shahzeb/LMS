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

        public virtual DbSet<LmsDefaultSystemSetting> LmsDefaultSystemSettings { get; set; } = null!;
        public virtual DbSet<LmsInventory> LmsInventories { get; set; } = null!;
        public virtual DbSet<LmsInventoryHistory> LmsInventoryHistories { get; set; } = null!;
        public virtual DbSet<LmsStaff> LmsStaffs { get; set; } = null!;
        public virtual DbSet<LmsStudent> LmsStudents { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-NGD8A62\\SHAZZ;Initial Catalog=LMS_DB;persist security info=True;User id=sa; Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LmsDefaultSystemSetting>(entity =>
            {
                entity.ToTable("LMS_DefaultSystemSetting");

                entity.Property(e => e.FinePerDay).HasColumnType("decimal(6, 2)");
            });

            modelBuilder.Entity<LmsInventory>(entity =>
            {
                entity.ToTable("LMS_Inventory");

                entity.Property(e => e.BookAuthor).HasMaxLength(50);

                entity.Property(e => e.BookCode).HasMaxLength(50);

                entity.Property(e => e.BookGenre).HasMaxLength(50);

                entity.Property(e => e.BookTitle).HasMaxLength(50);

                entity.Property(e => e.BookVersion).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LmsInventoryHistory>(entity =>
            {
                entity.ToTable("LMS_InventoryHistory");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EntryType).HasMaxLength(50);

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.LmsInventoryHistories)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LMS_InventoryHistory_LMS_Inventory");

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
