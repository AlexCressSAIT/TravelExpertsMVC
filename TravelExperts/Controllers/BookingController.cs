using DataManagerAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TravelExperts.Models;
using TravelExpertsData;

namespace TravelExperts.Controllers
{
    public class BookingController : Controller
    {
        // GET: BookingController
        public ActionResult Index()
        {
            // Create an anonymous 
            var packages = new[]
            {
                new
                {
                    Value = 0, Text = ""
                }
            }.ToList();
            packages.AddRange(
                PackageManager.GetPackages()
                .Select(p => new { Value = p.PackageId, Text = p.PkgName })
                .OrderBy(p => p.Text)
                );
            return View(packages);
        }

        [Route("/api/package/{packageId?}")]
        public Package GetPackage(int packageId)
        {
            return PackageManager.GetPackageById(packageId);
        }

        [Route("/api/package/gallery/{packageId?}")]
        public string[] GetPackagePhotos(int packageId)
        {
            string path = $@"{Directory.GetCurrentDirectory()}\wwwroot\media\images\vacations\{packageId}";
            if (Directory.Exists(path))
            {
                return Directory.EnumerateFiles(path)
                    .Select(p => $"/media/images/vacations/{packageId}/{Path.GetFileName(p)}")
                    .ToArray();
            }

            return new string[] { };
        }

        // GET: BookingController/Details/5
        public ActionResult ViewBookings(int id)
        {
            ViewBag.Customer = CustomerManager.GetCustomer(id);
            List<Booking> bookings = BookingManager.GetCustomerBookings(id).OrderByDescending(b => b.BookingDate).ToList();
            return View(bookings);
        }

        [HttpGet]
        public ActionResult Options(int packageId)
        {
            TempData["packageId"] = packageId;
            ViewBag.SelectNumTravelers = Utils.SelectRange(1, 10);
            List<TripType> tripTypes = TripTypeManager.GetTripTypes();
            ViewBag.SelectTripClass = new SelectList(tripTypes, "TripTypeId", "Ttname");
            ViewBag.PackageName = PackageManager.GetPackageName(packageId);
            return View();
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            int customerId = HttpContext.Session.GetInt32("CurrentCustomer") ?? 0;

            if (customerId < 1)
            {
                HttpContext.Session.SetObject<bool>("CustomerLoggedIn", false);
                return RedirectToAction("Login", "Account");
            }
            // Get the current booking session
            BookingSession session = new BookingSession(HttpContext.Session);

            // Get the cart items
            List<CartItemViewModel> items = session.GetCartItems();

            // If the cart is empty, redirect to the home page
            if (items.Count == 0)
                return RedirectToAction("Index", "Home");

            // Use a random uuid as a booking number to tie all the packages to one "booking"
            string bookingNo = Guid.NewGuid().ToString();

            // Create bookings from the cart data for each item
            List<Booking> bookings = items.Select(i =>
                new Booking {
                    BookingDate = DateTime.Now,
                    BookingNo = bookingNo,
                    PackageId = i.Package.PackageId,
                    TravelerCount = i.NumTravelers,
                    TripTypeId = i.TripTypeId,
                    CustomerId = customerId
                }
            ).ToList();

            // Add the bookings to the database
            BookingManager.AddBookings(bookings);

            // Clear the cart
            session.ClearCart();

            return View();
        }

        [HttpPost]
        public ActionResult FinalizeOptions(int packageId, int numTravelers, string tripTypeId)
        {
            CartItemViewModel cartItemView = CartItemViewModel.BuildCartItem(packageId, numTravelers, tripTypeId);

            return View(cartItemView);
        }

        // GET: BookingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingController/Create
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

        // GET: BookingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookingController/Edit/5
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

        // GET: BookingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookingController/Delete/5
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
