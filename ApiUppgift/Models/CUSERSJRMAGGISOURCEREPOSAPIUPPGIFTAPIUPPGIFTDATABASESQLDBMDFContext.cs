using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ApiUppgift.Models
{
    public partial class CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext : DbContext
    {
        public CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext()
        {
        }

        public CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext(DbContextOptions<CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<SessionToken> SessionTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\JR&MAGGI\\source\\repos\\ApiUppgift\\ApiUppgift\\Database\\sqldb.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.ToTable("Administrator");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.PasswordSalt).IsRequired();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.ToTable("Issue");

                entity.Property(e => e.IssueChange).HasColumnType("datetime");

                entity.Property(e => e.IssueStart).HasColumnType("datetime");

                entity.Property(e => e.IssueStatus)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.AdministratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Issue__Administr__276EDEB3");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Issue__CustomerI__286302EC");
            });

            modelBuilder.Entity<SessionToken>(entity =>
            {
                entity.ToTable("SessionToken");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccessToken).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
