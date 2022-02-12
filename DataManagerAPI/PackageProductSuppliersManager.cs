using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData;

namespace DataManagerAPI
{
    public static class PackageProductSuppliersManager
    {
        public static List<ProductsSupplier> GetProductSuppliers(Package package)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            return db.PackagesProductsSuppliers
                .Join(db.ProductsSuppliers,
                pps => pps.ProductSupplierId,
                ps => ps.ProductSupplierId,
                (pps, ps) => new { pps, ps }
                ).Where(o => o.pps.PackageId == package.PackageId)
                .Select(o => o.ps).ToList();
        }
    }
}
