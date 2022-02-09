using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    [Table("Products_Suppliers_Archive")]
    [Index(nameof(ProductSupplierId), Name = "ProductSupplierId_UQ00", IsUnique = true)]
    public partial class ProductsSuppliersArchive
    {
        [Key]
        public int ProductSupplierId { get; set; }

        [ForeignKey(nameof(ProductSupplierId))]
        [InverseProperty(nameof(ProductsSupplier.ProductsSuppliersArchive))]
        public virtual ProductsSupplier ProductSupplier { get; set; }
    }
}
