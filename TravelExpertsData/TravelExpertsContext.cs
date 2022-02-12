using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TravelExpertsData
{
    public partial class TravelExpertsContext : DbContext
    {
        public TravelExpertsContext()
        {
        }

        public TravelExpertsContext(DbContextOptions<TravelExpertsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Affiliation> Affiliations { get; set; }
        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<BookingDetail> BookingDetails { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<CreditCard> CreditCards { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomersReward> CustomersRewards { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Fee> Fees { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackagesProductsSupplier> PackagesProductsSuppliers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductsSupplier> ProductsSuppliers { get; set; }
        public virtual DbSet<ProductsSuppliersArchive> ProductsSuppliersArchives { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Reward> Rewards { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierContact> SupplierContacts { get; set; }
        public virtual DbSet<TripType> TripTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress;Initial Catalog=TravelExperts;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Affiliation>(entity =>
            {
                entity.HasKey(e => e.AffilitationId)
                    .HasName("aaaaaAffiliations_PK")
                    .IsClustered(false);
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.AgencyId)
                    .HasConstraintName("FK_Agents_Agencies");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("aaaaaBookings_PK")
                    .IsClustered(false);

                entity.Property(e => e.PackageId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("Bookings_FK00");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("Bookings_FK01");

                entity.HasOne(d => d.TripType)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.TripTypeId)
                    .HasConstraintName("Bookings_FK02");
            });

            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.HasKey(e => e.BookingDetailId)
                    .HasName("aaaaaBookingDetails_PK")
                    .IsClustered(false);

                entity.Property(e => e.BookingId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProductSupplierId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_BookingDetails_Bookings");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_BookingDetails_Classes");

                entity.HasOne(d => d.Fee)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.FeeId)
                    .HasConstraintName("FK_BookingDetails_Fees");

                entity.HasOne(d => d.ProductSupplier)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.ProductSupplierId)
                    .HasConstraintName("FK_BookingDetails_Products_Suppliers");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_BookingDetails_Regions");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("aaaaaClasses_PK")
                    .IsClustered(false);
            });

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.HasKey(e => e.CreditCardId)
                    .HasName("aaaaaCreditCards_PK")
                    .IsClustered(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CreditCards)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CreditCards_FK00");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("aaaaaCustomers_PK")
                    .IsClustered(false);

                entity.Property(e => e.CustPassword)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('password')");

                entity.Property(e => e.CustUsername)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('temp')");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_Customers_Agents");
            });

            modelBuilder.Entity<CustomersReward>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.RewardId })
                    .HasName("aaaaaCustomers_Rewards_PK")
                    .IsClustered(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomersRewards)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customers_Rewards_FK00");

                entity.HasOne(d => d.Reward)
                    .WithMany(p => p.CustomersRewards)
                    .HasForeignKey(d => d.RewardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customers_Rewards_FK01");
            });

            modelBuilder.Entity<Fee>(entity =>
            {
                entity.HasKey(e => e.FeeId)
                    .HasName("aaaaaFees_PK")
                    .IsClustered(false);
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasKey(e => e.PackageId)
                    .HasName("aaaaaPackages_PK")
                    .IsClustered(false);

                entity.Property(e => e.PkgAgencyCommission).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<PackagesProductsSupplier>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.ProductSupplierId })
                    .HasName("aaaaaPackages_Products_Suppliers_PK")
                    .IsClustered(false);

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackagesProductsSuppliers)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Packages_Products_Supplie_FK00");

                entity.HasOne(d => d.ProductSupplier)
                    .WithMany(p => p.PackagesProductsSuppliers)
                    .HasForeignKey(d => d.ProductSupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Packages_Products_Supplie_FK01");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("aaaaaProducts_PK")
                    .IsClustered(false);
            });

            modelBuilder.Entity<ProductsSupplier>(entity =>
            {
                entity.HasKey(e => e.ProductSupplierId)
                    .HasName("aaaaaProducts_Suppliers_PK")
                    .IsClustered(false);

                entity.Property(e => e.ProductId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsSuppliers)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("Products_Suppliers_FK00");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ProductsSuppliers)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("Products_Suppliers_FK01");
            });

            modelBuilder.Entity<ProductsSuppliersArchive>(entity =>
            {
                entity.HasKey(e => e.ProductSupplierId)
                    .HasName("ProductSupplierId_PK00");

                entity.Property(e => e.ProductSupplierId).ValueGeneratedNever();

                entity.HasOne(d => d.ProductSupplier)
                    .WithOne(p => p.ProductsSuppliersArchive)
                    .HasForeignKey<ProductsSuppliersArchive>(d => d.ProductSupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Products_Suppliers_Archive_FK00");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .HasName("aaaaaRegions_PK")
                    .IsClustered(false);
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.HasKey(e => e.RewardId)
                    .HasName("aaaaaRewards_PK")
                    .IsClustered(false);

                entity.Property(e => e.RewardId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId)
                    .HasName("aaaaaSuppliers_PK")
                    .IsClustered(false);

                entity.Property(e => e.SupplierId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SupplierContact>(entity =>
            {
                entity.HasKey(e => e.SupplierContactId)
                    .HasName("aaaaaSupplierContacts_PK")
                    .IsClustered(false);

                entity.Property(e => e.SupplierContactId).ValueGeneratedNever();

                entity.Property(e => e.SupplierId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Affiliation)
                    .WithMany(p => p.SupplierContacts)
                    .HasForeignKey(d => d.AffiliationId)
                    .HasConstraintName("SupplierContacts_FK00");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierContacts)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("SupplierContacts_FK01");
            });

            modelBuilder.Entity<TripType>(entity =>
            {
                entity.HasKey(e => e.TripTypeId)
                    .HasName("aaaaaTripTypes_PK")
                    .IsClustered(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
