using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;

namespace TravelExperts.Models
{
    public class BookingSession
    {
        private const string PRODUCTS_KEY = "productList";
        private const string PACKAGES_KEY = "packageList";
        private ISession session { get; set; }
        public BookingSession(ISession session)
        {
            this.session = session;
        }

        public void SetProducts(List<Product> products)
        {
            session.SetObject(PRODUCTS_KEY, products);
        }

        public void SetPackages(List<Package> packages)
        {
            session.SetObject(PACKAGES_KEY, packages);
        }

        public List<Product> GetProducts()
        {
            return session.GetObject<List<Product>>(PRODUCTS_KEY) ?? new List<Product>();
        }

        public List<Package> GetPackages()
        {
            return session.GetObject<List<Package>>(PACKAGES_KEY) ?? new List<Package>();
        }
    }
}
