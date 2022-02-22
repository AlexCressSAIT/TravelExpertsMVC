using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

// Updated by: Alex Cress -Added validation/error messages to fields (regex)

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

        [Required(ErrorMessage = "First name is required")]
        [StringLength(25)]
        [Display(Name = "First Name")]
        public string CustFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
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

        [RegularExpression(@"^[ABCEGHJ-NPRSTVXY][0-9][ABCEGHJ-NPRSTV-Z][ ]?[0-9][ABCEGHJ-NPRSTV-Z][0-9]$", ErrorMessage = "Please enter a valid Postal Code. Example: A1B 2C3")]
        [Required(ErrorMessage = "Postal code is required")]
        [StringLength(7)]
        [Display(Name = "Postal/Zip Code")]
        public string CustPostal { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(25)]
        [Display(Name = "Country")]
        public string CustCountry { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid phone number with area code. Example: (111)222-3333 or 1112223333")]
        [StringLength(13)]
        [Display(Name = "Home Phone")]
        public string CustHomePhone { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid phone number with area code. Example: (111)222-3333 or 1112223333")]
        [StringLength(13)]
        [Display(Name = "Business Phone")]
        public string CustBusPhone { get; set; }

        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$",
                            ErrorMessage = "Please enter a valid email address. Example: yourname@mail.com")]
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

        [NotMapped]
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
