using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelExperts.Models
{
    public class PackageBookingGroupModel
    {
        public List<PackageBookingModel> Bookings = new List<PackageBookingModel>();
        public DateTime? BookingDate { get; set; }
    }
}
