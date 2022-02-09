using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    [Index(nameof(CustomerId), Name = "CustomersCreditCards")]
    public partial class CreditCard
    {
        [Key]
        public int CreditCardId { get; set; }
        [Required]
        [Column("CCName")]
        [StringLength(10)]
        public string Ccname { get; set; }
        [Required]
        [Column("CCNumber")]
        [StringLength(50)]
        public string Ccnumber { get; set; }
        [Column("CCExpiry", TypeName = "datetime")]
        public DateTime Ccexpiry { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("CreditCards")]
        public virtual Customer Customer { get; set; }
    }
}
