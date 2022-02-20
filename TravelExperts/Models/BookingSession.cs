using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;

namespace TravelExperts.Models
{
    /*
     * Model class representing a booking session (holds the cart items)
     * Author: Nate Penner
     * February 2022
     */
    public class BookingSession
    {
        private const string CART_ITEMS_KEY = "cartItems";  // The session variable name for the cart items
        private ISession session { get; set; }  // Stores the original session passed to this instance
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="session">Inject ISession dependency</param>
        public BookingSession(ISession session)
        {
            this.session = session;
        }

        /// <summary>
        /// Serializes a list of cart items and stores in the session
        /// </summary>
        /// <param name="cartItems">The list of items to serialize</param>
        public void SetCartItems(List<CartItemViewModel> cartItems)
        {
            session.SetObject("cartItems", cartItems);
        }

        /// <summary>
        /// Deserializes a list of cart items from the session
        /// </summary>
        /// <returns>A list of cart items</returns>
        public List<CartItemViewModel> GetCartItems()
        {
            // If object is null or not defined in the session, return a new list
            return session.GetObject<List<CartItemViewModel>>(CART_ITEMS_KEY) ?? new List<CartItemViewModel>();
        }

        /// <summary>
        /// Empties the cart by clearing the session storage of cart items and serializing
        /// an empty list
        /// </summary>
        public void ClearCart()
        {
            session.SetObject("cartItems", new List<CartItemViewModel>());
        }
    }
}
