using DataManagerAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TravelExperts.Models;

namespace TravelExperts.Controllers
{
    public class CartController : Controller
    {
        // GET: CartController
        [Route("{controller}")]
        public ActionResult ViewCart()
        {
            BookingSession session = new BookingSession(HttpContext.Session);
            List<CartItemViewModel> model = session.GetCartItems();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddItem(int packageId)
        {
            return RedirectToAction("Options", "Booking", new { packageId = packageId });
        }
        
        [HttpPost]
        public ActionResult SaveCartItem(int packageId, int numTravelers, string tripTypeId)
        {
            BookingSession session = new BookingSession(HttpContext.Session);
            List<CartItemViewModel> cart = session.GetCartItems();
            cart.Add(
                CartItemViewModel.BuildCartItem(packageId, numTravelers, tripTypeId)
                );
            session.SetCartItems(cart);

            return RedirectToAction("ViewCart");
        }

        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
