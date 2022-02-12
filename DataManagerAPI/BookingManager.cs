using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData;

namespace DataManagerAPI
{
    public static class BookingManager
    {
        public static List<Booking> GetCustomerBookings(int customerId)
        {
            TravelExpertsContext db = new TravelExpertsContext();

            return db.Bookings.Where(b => b.CustomerId == customerId).ToList();
        }

        public static void AddBooking(Booking booking)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            booking.BookingDate = DateTime.Now;
            booking.BookingNo = "asdfas";
            booking.CustomerId = 121;
            booking.TravelerCount = 3;
            booking.TripTypeId = "B";
            booking.PackageId = 1;
            db.Bookings.Add(booking);
            db.SaveChanges();
        }
    }
}
