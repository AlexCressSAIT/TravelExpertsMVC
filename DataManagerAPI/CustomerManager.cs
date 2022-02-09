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
    }
}
