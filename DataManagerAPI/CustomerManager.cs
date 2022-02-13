using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData;

namespace DataManagerAPI
{
    /// <summary>
    /// Handles all database interactions with the Customer table
    /// </summary>
    public static class CustomerManager
    {
        public static Customer GetCustomer(int id)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            return db.Customers.Find(id);
        }

        public static List<Customer> GetCustomersWithBookings()
        {
            TravelExpertsContext db = new TravelExpertsContext();

            return db.Customers.Include(c => c.Bookings).Where(c => c.Bookings.Count > 0).ToList();
        }

        public static List<Customer> GetCustomerList()
        {
            TravelExpertsContext db = new TravelExpertsContext();

            return db.Customers.ToList();
        }
        /// <summary>
        /// Add provided customer to database
        /// </summary>
        /// <param name="customer"></param>
        /// <author>Daniel Palmer</author>
        public static void AddCustomer(Customer customer)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public static Customer Authenticate(string username, string password)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            return db.Customers.SingleOrDefault(c => c.CustUsername == username && c.CustPassword == password);
        }
    }
}