using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData;

namespace DataManagerAPI
{
    public static class PackageManager
    {
        public static List<Package> GetPackages()
        {
            TravelExpertsContext db = new TravelExpertsContext();
            return db.Packages.ToList();
        }

        public static Package GetPackageById(int packageId)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            return db.Packages.SingleOrDefault(p => p.PackageId == packageId);
        }

        public static string GetPackageName(int packageId)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            return db.Packages.Where(p => p.PackageId == packageId).Select(p => p.PkgName)
                .SingleOrDefault();
        }
    }
}
