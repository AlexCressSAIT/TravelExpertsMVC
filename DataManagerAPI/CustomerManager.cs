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
        public static Customer AddCustomer(Customer customer)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            var customerContext = db.Customers.Add(customer);
            db.SaveChanges();
            return customerContext.Entity;
        }

        public static Customer Authenticate(string username, string password)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            return db.Customers.SingleOrDefault(c => c.CustUsername == username && c.CustPassword == password);
        }

        public static void UpdateCustomer(Customer newCustomer)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            Customer oldCustomer = db.Customers.Find(newCustomer.CustomerId);

            oldCustomer.CustFirstName = newCustomer.CustFirstName;
            oldCustomer.CustLastName = newCustomer.CustLastName;
            oldCustomer.CustUsername = newCustomer.CustUsername;
            oldCustomer.CustAddress = newCustomer.CustAddress;
            oldCustomer.CustCity = newCustomer.CustCity;
            oldCustomer.CustProv = newCustomer.CustProv;
            oldCustomer.CustPostal = newCustomer.CustPostal;
            oldCustomer.CustCountry = newCustomer.CustCountry;
            oldCustomer.CustHomePhone = newCustomer.CustHomePhone;
            oldCustomer.CustBusPhone = newCustomer.CustBusPhone;
            oldCustomer.CustEmail = newCustomer.CustEmail;

            db.Customers.Update(oldCustomer);
            db.SaveChanges();
        }
    }
}