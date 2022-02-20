using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;

namespace TravelExperts.Models
{
    /*
     * A view model for a booked package
     * Author: Nate Penner
     * February 2022
     */
    public class PackageBookingModel
    {
        // The package object
        public Package Package { get; set; }

        // Number of people traveling
        public int TravelerCount { get; set; }

        // Trip type name (business, group, leisure)
        public string TripTypeName { get; set; }

        // ID of the associated booking
        public int BookingId { get; set; }
    }
}