using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    [Keyless]
    public partial class Employee
    {
        [Required]
        [StringLength(20)]
        public string EmpFirstName { get; set; }
        [StringLength(5)]
        public string EmpMiddleInitial { get; set; }
        [Required]
        [StringLength(20)]
        public string EmpLastName { get; set; }
        [Required]
        [StringLength(20)]
        public string EmpBusPhone { get; set; }
        [Required]
        [StringLength(50)]
        public string EmpEmail { get; set; }
        [Required]
        [StringLength(20)]
        public string EmpPosition { get; set; }
    }
}
