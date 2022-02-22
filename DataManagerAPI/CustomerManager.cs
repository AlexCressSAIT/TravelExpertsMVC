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
        /// <summary>
        /// Gets Customer based on passed Id
        /// </summary>
        /// <param name="id"> Customer Id </param>
        /// <returns>A Customer Associated with the id </returns>
        /// <author>Daniel Palmer</author>
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
        //Updated by: Alex Cress -Added formatting
        //                       -Return customer after complete
        /// <summary>
        /// Add provided customer to database
        /// </summary>
        /// <param name="customer"></param>
        /// <author>Daniel Palmer</author>
        public static Customer AddCustomer(Customer customer)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            //Format for DB
            customer.CustHomePhone = FormatPhone(customer.CustHomePhone);
            customer.CustBusPhone = FormatPhone(customer.CustBusPhone);
            customer.CustPostal = FormatPostal(customer.CustPostal);

            var customerContext = db.Customers.Add(customer);
            db.SaveChanges();
            return customerContext.Entity;
        }
        /// <summary>
        /// Verifies that a Customer exists with passed username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>A Customer Associated with the username and password </returns>
        /// <author>Daniel Palmer</author>
        public static Customer Authenticate(string username, string password)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            return db.Customers.SingleOrDefault(c => c.CustUsername == username && c.CustPassword == password);
        }
        /// <summary>
        /// Verifies the Username is unique
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Returns a bool whether the username is unique</returns>
        public static bool VerifyUsername(string username)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            if (db.Customers.SingleOrDefault(usr => usr.CustUsername == username) != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Author: Alex Cress
        /// <summary>
        /// Updates an existing customer. The newCustomer object must have CustomerId set.
        /// </summary>
        /// <param name="newCustomer">the Customer you wish to update</param>
        public static void UpdateCustomer(Customer newCustomer)
        {
            //Get reference
            TravelExpertsContext db = new TravelExpertsContext();
            Customer oldCustomer = db.Customers.Find(newCustomer.CustomerId);

            //Update all info
            oldCustomer.CustFirstName = newCustomer.CustFirstName;
            oldCustomer.CustLastName = newCustomer.CustLastName;
            oldCustomer.CustUsername = newCustomer.CustUsername;
            oldCustomer.CustAddress = newCustomer.CustAddress;
            oldCustomer.CustCity = newCustomer.CustCity;
            oldCustomer.CustProv = newCustomer.CustProv;
            oldCustomer.CustPostal = FormatPostal(newCustomer.CustPostal);
            oldCustomer.CustCountry = newCustomer.CustCountry;
            oldCustomer.CustHomePhone = FormatPhone(newCustomer.CustHomePhone);
            oldCustomer.CustBusPhone = FormatPhone(newCustomer.CustBusPhone);
            oldCustomer.CustEmail = newCustomer.CustEmail;

            db.Customers.Update(oldCustomer);
            db.SaveChanges();
        }

        // Author: Alex Cress
        /// <summary>
        /// Removes optional formatting to a phone number string. This includes '(' ')' '-'
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>a string with all formatting removed</returns>
        private static string FormatPhone(string phone)
        {
            if (String.IsNullOrEmpty(phone))
            {
                return phone;
            }
            return phone.Replace("(", "").Replace(")", "").Replace("-", "");
        }

        // Author: Alex Cress
        /// <summary>
        /// Formats a postal code for insertion into the database.
        /// </summary>
        /// <param name="postal"></param>
        /// <returns>the formatted string</returns>
        private static string FormatPostal(string postal)
        {
            if (!String.IsNullOrEmpty(postal) && postal.Length == 6)
            {
                postal = postal.Insert(3, " ");
            }
            
            return postal;
        }
    }
}