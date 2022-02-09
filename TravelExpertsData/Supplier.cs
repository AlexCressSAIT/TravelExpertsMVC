using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    [Index(nameof(SupplierId), Name = "SupplierId")]
    public partial class Supplier
    {
        public Supplier()
        {
            ProductsSuppliers = new HashSet<ProductsSupplier>();
            SupplierContacts = new HashSet<SupplierContact>();
        }

        [Key]
        public int SupplierId { get; set; }
        [StringLength(255)]
        public string SupName { get; set; }

        [InverseProperty(nameof(ProductsSupplier.Supplier))]
        public virtual ICollection<ProductsSupplier> ProductsSuppliers { get; set; }
        [InverseProperty(nameof(SupplierContact.Supplier))]
        public virtual ICollection<SupplierContact> SupplierContacts { get; set; }
    }
}
