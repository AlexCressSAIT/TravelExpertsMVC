using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    [Table("Products_Suppliers")]
    [Index(nameof(SupplierId), Name = "Product Supplier ID")]
    [Index(nameof(ProductId), Name = "ProductId")]
    [Index(nameof(ProductSupplierId), Name = "ProductSupplierId")]
    [Index(nameof(ProductId), Name = "ProductsProducts_Suppliers1")]
    [Index(nameof(SupplierId), Name = "SuppliersProducts_Suppliers1")]
    public partial class ProductsSupplier
    {
        public ProductsSupplier()
        {
            BookingDetails = new HashSet<BookingDetail>();
            PackagesProductsSuppliers = new HashSet<PackagesProductsSupplier>();
        }

        [Key]
        public int ProductSupplierId { get; set; }
        public int? ProductId { get; set; }
        public int? SupplierId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductsSuppliers")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("ProductsSuppliers")]
        public virtual Supplier Supplier { get; set; }
        [InverseProperty("ProductSupplier")]
        public virtual ProductsSuppliersArchive ProductsSuppliersArchive { get; set; }
        [InverseProperty(nameof(BookingDetail.ProductSupplier))]
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
        [InverseProperty(nameof(PackagesProductsSupplier.ProductSupplier))]
        public virtual ICollection<PackagesProductsSupplier> PackagesProductsSuppliers { get; set; }
    }
}
