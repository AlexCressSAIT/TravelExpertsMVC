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
        [Route("{controller}")]
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

        // GET: /Booking/4
        [HttpGet]
        [Route("{controller}/{action}/{id}")]
        public ActionResult ViewBookings(int id)
        {
            int loggedInId = HttpContext.Session.GetInt32("CurrentCustomer") ?? 0;

            if (loggedInId == 0)
            {
                HttpContext.Session.SetObject<string>("LoginErrorMessage", "You must log in to view your bookings.");
                return RedirectToAction("Login", "Account");
            }

            ViewBag.CustomerId = loggedInId;
            /*List<Booking> bookings = BookingManager.GetCustomerBookings(id).OrderByDescending(b => b.BookingDate).ToList();*/
            
            TravelExpertsContext db = new TravelExpertsContext();
            
            // Get a list of all bookings for a customer, grouped by booking number
            List<List<Booking>> bookingGroups = BookingManager.GetGroupedPackageBookingsByCustomerId(loggedInId, db);
            
            // View model
            PackageBookingGroupViewModel model = new PackageBookingGroupViewModel();

            // Loop through the groups and bookings and build the view model
            bookingGroups.ForEach(bg =>
            {
                // New group (all bookings of the same booking number have the same booking date)
                PackageBookingGroupModel groupModel = new PackageBookingGroupModel
                {
                    BookingDate = bg[0].BookingDate     // just use the booking date from the first item
                };

                // Save the groupModel to the view
                model.BookingGroup.Add(groupModel);

                // loop through the booking list and create package booking models
                bg.ForEach(b =>
                {
                    groupModel.Bookings.Add(new PackageBookingModel
                    {
                        Package = PackageManager.GetPackageById((int)b.PackageId),
                        TravelerCount = (int)b.TravelerCount,
                        TripTypeName = b.TripTypeId == null ? null : TripTypeManager.GetTripTypeNameById(b.TripTypeId, db),
                        BookingId = b.BookingId
                    });
                    groupModel.Bookings = groupModel.Bookings.OrderBy(gmb => gmb.BookingId).ToList();
                });
            });

            model.BookingGroup = model.BookingGroup.OrderByDescending(bg => bg.BookingDate).ToList();
            return View(model);
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
                HttpContext.Session.SetObject<string>("LoginErrorMessage", "You must be logged in to book vacation packages");
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
            ViewBag.Customer = customerId;
            return View();
        }

        [HttpPost]
        public ActionResult FinalizeOptions(int packageId, int numTravelers, string tripTypeId)
        {
            CartItemViewModel cartItemView = CartItemViewModel.BuildCartItem(packageId, numTravelers, tripTypeId);
            DateTime sd = (DateTime)cartItemView.Package.PkgStartDate;
            DateTime ed = (DateTime)cartItemView.Package.PkgEndDate;
            ViewBag.Sd = sd.ToString("MMMM dd, yyyy");
            ViewBag.Ed = ed.ToString("MMMM dd, yyyy");

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
