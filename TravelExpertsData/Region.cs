using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    public partial class Region
    {
        public Region()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        [Key]
        [StringLength(5)]
        public string RegionId { get; set; }
        [StringLength(25)]
        public string RegionName { get; set; }

        [InverseProperty(nameof(BookingDetail.Region))]
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
