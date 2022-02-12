using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;

namespace TravelExperts.Models
{
    public class CartViewModel
    {
        public List<Product> Products = new List<Product>();
        public List<Package> Packages = new List<Package>();

        public bool IsEmpty()
        {
            return Products.Count < 1 && Packages.Count < 1;
        }
    }
}
