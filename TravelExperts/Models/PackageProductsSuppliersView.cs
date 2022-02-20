using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;

namespace TravelExperts.Models
{
    /*
     * A view for storing the name of a product and an associated supplier's name
     * Author: Nate Penner
     * February 2022
     */
    public class PackageProductsSuppliersView
    {
        // Name of the product
        public string ProductName { get; set; }

        // Name of the supplier
        public string SupplierName { get; set; }
    }
}
