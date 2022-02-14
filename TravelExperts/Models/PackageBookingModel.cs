using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;

namespace TravelExperts.Models
{
    public class PackageBookingModel
    {
        public Package Package { get; set; }
        public int TravelerCount { get; set; }
        public string TripTypeName { get; set; }
        public int BookingId { get; set; }
    }
}