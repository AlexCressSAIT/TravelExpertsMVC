using DataManagerAPI;
using System;
using System.Linq;
using TravelExpertsData;

namespace TravelExperts.Models
{
    public class CartItemViewModel
    {
        public Package Package { get; set; }
        public int NumTravelers { get; set; }
        public string TripTypeId { get; set; }
        public string TripTypeName { get; set; }
        public int TripDuration { get; set; }

        public static CartItemViewModel BuildCartItem(int packageId, int numTravelers, string tripTypeId)
        {
            Package pkg = PackageManager.GetPackageById(packageId);
            TripType tt = TripTypeManager.GetTripTypes().Where(tt => tt.TripTypeId == tripTypeId).SingleOrDefault();

            return new CartItemViewModel
            {
                Package = pkg,
                NumTravelers = numTravelers,
                TripTypeId = tt.TripTypeId,
                TripTypeName = tt.Ttname,
                TripDuration = ((TimeSpan)(pkg.PkgEndDate - pkg.PkgStartDate)).Days
            };
        }
    }
}
