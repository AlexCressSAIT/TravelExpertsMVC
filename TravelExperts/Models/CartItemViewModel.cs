using DataManagerAPI;
using System;
using System.Linq;
using TravelExpertsData;

namespace TravelExperts.Models
{
    /*
     * A view model representing an item in the cart
     */
    public class CartItemViewModel
    {
        public Package Package { get; set; }    // the package ID of this item
        public Guid CartItemKey { get; set; }
        public int NumTravelers { get; set; }   // the number of travelers
        public string TripTypeId { get; set; }  // the ID of the trip type
        public string TripTypeName { get; set; }    // the name of the trip type
        public int TripDuration { get; set; }   // duration of this trip

        /// <summary>
        /// Generates a view model instance of this object from the info received
        /// </summary>
        /// <param name="packageId">The package ID being booked</param>
        /// <param name="numTravelers">The number of travelers</param>
        /// <param name="tripTypeId">The trip type id</param>
        /// <returns>A view model for this cart item</returns>
        public static CartItemViewModel BuildCartItem(int packageId, int numTravelers, string tripTypeId, Guid? guid = null)
        {
            // Retrieve the package from the database by ID
            Package pkg = PackageManager.GetPackageById(packageId);

            // Get trip type from the database by trip type id
            TripType tt = TripTypeManager.GetTripTypes().Where(tt => tt.TripTypeId == tripTypeId).SingleOrDefault();

            // return the new item view model
            return new CartItemViewModel
            {
                CartItemKey = guid ?? Guid.NewGuid(),
                Package = pkg,
                NumTravelers = numTravelers,
                TripTypeId = tt.TripTypeId,
                TripTypeName = tt.Ttname,
                TripDuration = ((TimeSpan)(pkg.PkgEndDate - pkg.PkgStartDate)).Days     // Calculate trip duration
            };
        }
    }
}
