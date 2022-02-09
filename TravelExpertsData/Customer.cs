using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    [Index(nameof(AgentId), Name = "EmployeesCustomers")]
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            CreditCards = new HashSet<CreditCard>();
            CustomersRewards = new HashSet<CustomersReward>();
        }

        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(25)]
        public string CustFirstName { get; set; }
        [Required]
        [StringLength(25)]
        public string CustLastName { get; set; }
        [Required]
        [StringLength(75)]
        public string CustAddress { get; set; }
        [Required]
        [StringLength(50)]
        public string CustCity { get; set; }
        [Required]
        [StringLength(2)]
        public string CustProv { get; set; }
        [Required]
        [StringLength(7)]
        public string CustPostal { get; set; }
        [StringLength(25)]
        public string CustCountry { get; set; }
        [StringLength(20)]
        public string CustHomePhone { get; set; }
        [Required]
        [StringLength(20)]
        public string CustBusPhone { get; set; }
        [Required]
        [StringLength(50)]
        public string CustEmail { get; set; }
        public int? AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        [InverseProperty("Customers")]
        public virtual Agent Agent { get; set; }
        [InverseProperty(nameof(Booking.Customer))]
        public virtual ICollection<Booking> Bookings { get; set; }
        [InverseProperty(nameof(CreditCard.Customer))]
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        [InverseProperty(nameof(CustomersReward.Customer))]
        public virtual ICollection<CustomersReward> CustomersRewards { get; set; }
    }
}
