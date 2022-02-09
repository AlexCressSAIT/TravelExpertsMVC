using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    [Table("Customers_Rewards")]
    [Index(nameof(CustomerId), Name = "CustomersCustomers_Rewards")]
    [Index(nameof(RewardId), Name = "RewardsCustomers_Rewards")]
    public partial class CustomersReward
    {
        [Key]
        public int CustomerId { get; set; }
        [Key]
        public int RewardId { get; set; }
        [Required]
        [StringLength(25)]
        public string RwdNumber { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("CustomersRewards")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(RewardId))]
        [InverseProperty("CustomersRewards")]
        public virtual Reward Reward { get; set; }
    }
}
