using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData;

namespace DataManagerAPI
{
    public static class ProductManager
    {
        public static List<Product> GetProducts()
        {
            TravelExpertsContext db = new TravelExpertsContext();

            return db.Products.ToList();
        }

        public static void AddProduct(Product product)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            db.Products.Add(product);
            db.SaveChanges();
        }

        public static Product GetProduct(ProductsSupplier productsSupplier)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            return db.Products.SingleOrDefault(p => p.ProductId == productsSupplier.ProductId);
        }
    }
}
