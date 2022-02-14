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

        public static void AddBooking(Booking booking, TravelExpertsContext db = null)
        {
            db ??= new TravelExpertsContext();
            db.Bookings.Add(booking);
            db.SaveChanges();
        }

        public static void AddBookings(List<Booking> bookings)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            bookings.ForEach(b => AddBooking(b, db));
        }

        // ref: https://stackoverflow.com/a/9231743/7953750
        public static List<List<Booking>> GetGroupedPackageBookingsByCustomerId(int customerId, TravelExpertsContext db = null)
        {
            db ??= new TravelExpertsContext();
            List<List<Booking>> bookings = db.Bookings.Where(b => b.CustomerId == customerId && b.PackageId != null)
                .ToList().GroupBy(g => g.BookingNo).Select(g => g.ToList()).ToList();

            return bookings;
        }
    }
}
