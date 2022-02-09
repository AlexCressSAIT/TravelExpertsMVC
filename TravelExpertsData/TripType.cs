using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    public partial class TripType
    {
        public TripType()
        {
            Bookings = new HashSet<Booking>();
        }

        [Key]
        [StringLength(1)]
        public string TripTypeId { get; set; }
        [Column("TTName")]
        [StringLength(25)]
        public string Ttname { get; set; }

        [InverseProperty(nameof(Booking.TripType))]
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
