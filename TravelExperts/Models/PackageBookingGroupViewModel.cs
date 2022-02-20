using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelExperts.Models
{
    /*
     * A view containing a list of package booking groups. This is the top-level view; the BookingGroup
     * is a list of package bookings grouped together based on the booking NO generated when one or more
     * packages were booked together at the same time.
     * Author: Nate Penner
     * February 2022
     */
    public class PackageBookingGroupViewModel
    {
        public List<PackageBookingGroupModel> BookingGroup = new List<PackageBookingGroupModel>();
    }
}
