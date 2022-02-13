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
        [Display(Name = "Account No.")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(25)]
        [Display(Name = "First Name")]
        public string CustFirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(25)]
        [Display(Name = "Last Name")]
        public string CustLastName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(75)]
        [Display(Name = "Address")]
        public string CustAddress { get; set; }
        [Required(ErrorMessage = "City is required")]
        [StringLength(50)]
        [Display(Name = "City")]
        public string CustCity { get; set; }
        [Required(ErrorMessage = "Province is required")]
        [StringLength(2)]
        [Display(Name = "Provice/State")]
        public string CustProv { get; set; }
        [Required(ErrorMessage = "Postal code is required")]
        [StringLength(7)]
        [Display(Name = "Postal/Zip Code")]
        public string CustPostal { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [StringLength(25)]
        [Display(Name = "Country")]
        public string CustCountry { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(20)]
        [Display(Name = "Home Phone")]
        public string CustHomePhone { get; set; }
        [StringLength(20)]
        [Display(Name = "Business Phone")]
        public string CustBusPhone { get; set; }
        [StringLength(50)]
        [Display(Name = "Email")]
        public string CustEmail { get; set; }
        public int? AgentId { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50)]
        [Display(Name = "Username")]
        public string CustUsername { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50)]
        [Display(Name = "Password")]
        public string CustPassword { get; set; }
        [Display(Name="Confirm Password")]
        [Compare("CustPassword", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

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
