using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelExperts.Models
{
    /*
     * Package booking group view model. Allows user to view their package bookings
     * grouped together that were booked together in the same checkout operation
     * Author: Nate Penner
     * February 2022
     */
    public class PackageBookingGroupModel
    {
        // a list of bookings for this group
        public List<PackageBookingModel> Bookings = new List<PackageBookingModel>();
        
        // the date this booking was created
        public DateTime? BookingDate { get; set; }
    }
}
