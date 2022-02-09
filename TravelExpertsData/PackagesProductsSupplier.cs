using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    [Table("Packages_Products_Suppliers")]
    [Index(nameof(PackageId), Name = "PackagesPackages_Products_Suppliers")]
    [Index(nameof(ProductSupplierId), Name = "ProductSupplierId")]
    [Index(nameof(ProductSupplierId), Name = "Products_SuppliersPackages_Products_Suppliers")]
    public partial class PackagesProductsSupplier
    {
        [Key]
        public int PackageId { get; set; }
        [Key]
        public int ProductSupplierId { get; set; }

        [ForeignKey(nameof(PackageId))]
        [InverseProperty("PackagesProductsSuppliers")]
        public virtual Package Package { get; set; }
        [ForeignKey(nameof(ProductSupplierId))]
        [InverseProperty(nameof(ProductsSupplier.PackagesProductsSuppliers))]
        public virtual ProductsSupplier ProductSupplier { get; set; }
    }
}
