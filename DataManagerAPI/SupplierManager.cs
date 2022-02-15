using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData;

namespace DataManagerAPI
{
    public static class SupplierManager
    {
        public static Supplier GetSupplier(ProductsSupplier productsSupplier, TravelExpertsContext db = null)
        {
            db ??= new TravelExpertsContext();
            return db.Suppliers.SingleOrDefault(s => s.SupplierId == productsSupplier.SupplierId);
        }
    }
}
