using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    [Index(nameof(ProductId), Name = "ProductId")]
    public partial class Product
    {
        public Product()
        {
            ProductsSuppliers = new HashSet<ProductsSupplier>();
        }

        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProdName { get; set; }

        [InverseProperty(nameof(ProductsSupplier.Product))]
        public virtual ICollection<ProductsSupplier> ProductsSuppliers { get; set; }
    }
}
