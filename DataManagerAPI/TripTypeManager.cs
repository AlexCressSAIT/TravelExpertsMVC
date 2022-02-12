using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData;

namespace DataManagerAPI
{
    public static class TripTypeManager
    {
        public static List<TripType> GetTripTypes()
        {
            TravelExpertsContext db = new TravelExpertsContext();

            return db.TripTypes.OrderBy(tt => tt.Ttname).ToList();
        }
    }
}
