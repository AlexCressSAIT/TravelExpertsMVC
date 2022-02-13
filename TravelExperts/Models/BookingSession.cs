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
        private const string CART_ITEMS_KEY = "cartItems";
        private ISession session { get; set; }
        public BookingSession(ISession session)
        {
            this.session = session;
        }

        public void SetCartItems(List<CartItemViewModel> cartItems)
        {
            session.SetObject("cartItems", cartItems);
        }

        public List<CartItemViewModel> GetCartItems()
        {
            return session.GetObject<List<CartItemViewModel>>(CART_ITEMS_KEY) ?? new List<CartItemViewModel>();
        }
    }
}
