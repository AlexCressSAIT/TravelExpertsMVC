using DataManagerAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using TravelExperts.Models;

namespace TravelExperts.Controllers
{
    /*
     * The controller that handles cart-related functionality
     * Author: Nate Penner
     * February 2022
     */
    public class CartController : Controller
    {
        /// <summary>
        /// The action that shows the user their cart
        /// </summary>
        /// <returns>A list of the items in the cart</returns>
        // GET: CartController
        [Route("{controller}")]
        public ActionResult ViewCart()
        {
            // Load the session data, grab cart items and return the cart view
            BookingSession session = new BookingSession(HttpContext.Session);
            List<CartItemViewModel> model = session.GetCartItems();
            return View(model);
        }

        /// <summary>
        /// The action that redirects the user to the options page with the package they selected.
        /// The item is not yet saved to the cart at this point.
        /// </summary>
        /// <param name="packageId">The package id that will be added to the cart after options are selected</param>
        /// <returns>Redirects to booking options, passing the package ID to it as a query parameter</returns>
        [HttpPost]
        public ActionResult AddItem(int packageId)
        {
            return RedirectToAction("Options", "Booking", new { packageId = packageId });
        }
        
        /// <summary>
        /// Saves the package to the cart
        /// </summary>
        /// <param name="packageId">The id of the package to save</param>
        /// <param name="numTravelers">The number of people traveling</param>
        /// <param name="tripTypeId">The type of the trip</param>
        /// <returns>Redirects to the cart page</returns>
        [HttpPost]
        public ActionResult SaveCartItem(int packageId, int numTravelers, string tripTypeId)
        {
            // Grab the session data
            BookingSession session = new BookingSession(HttpContext.Session);

            // Add an item to the cart
            List<CartItemViewModel> cart = session.GetCartItems();
            cart.Add(
                CartItemViewModel.BuildCartItem(packageId, numTravelers, tripTypeId)
                );
            session.SetCartItems(cart);

            // Show the cart
            return RedirectToAction("ViewCart");
        }

        [HttpGet]
        public ActionResult DeleteCartItem(Guid cartItemId)
        {
            BookingSession session = new BookingSession(HttpContext.Session);

            List<CartItemViewModel> cart = session.GetCartItems();
            cart = cart.Where(i => i.CartItemKey != cartItemId).ToList();
            session.SetCartItems(cart);
            
            return RedirectToAction("ViewCart");
        }

        /// <summary>
        /// Clears the cart (removes all items)
        /// </summary>
        /// <returns>The view cart page</returns>
        [HttpGet]
        public ActionResult ClearAll()
        {
            // Clear the cart from the session
            BookingSession session = new BookingSession(HttpContext.Session);

            session.ClearCart();

            return View("ViewCart");
        }
    }
}
