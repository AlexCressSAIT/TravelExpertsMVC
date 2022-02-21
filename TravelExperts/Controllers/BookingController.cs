using DataManagerAPI;
using Microsoft.AspNetCore.Authorization;
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
    /*
     * The controller that handles all booking related pages and API routes
     * 
     * Author: Nate Penner
     * February 2022
     */
    public class BookingController : Controller
    {
        /// <summary>
        /// Controller for the main booking page
        /// </summary>
        /// <returns>The main booking page view</returns>
        // GET: BookingController
        [Route("{controller}")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// An API endpoint to assist with generating a client-side select list for the packages
        /// </summary>
        /// <returns>
        ///     A list of objects representing the select list options displaying package name
        ///     and using the package ID for the list option value
        /// </returns>
        [Route("/api/package/listoptions")]
        public Object GetPackageListOptions()
        {
            // Create the first select list entry
            var packages = new[]
            {
                new
                {
                    Value = 0, Text = "&lt;Select a package&gt;"
                }
            }.ToList();

            // Get the package info from the database for the select list
            packages.AddRange(
                PackageManager.GetPackages()
                .Select(p => new { Value = p.PackageId, Text = p.PkgName })
                .OrderBy(p => p.Text)
                );
            return packages;
        }

        /// <summary>
        /// An API endpoint for retrieving information about a specific vacation package by its packageId
        /// </summary>
        /// <param name="packageId">The ID of the package to retrieve</param>
        /// <returns>The package info</returns>
        [Route("/api/package/{packageId?}")]
        public Object GetPackage(int packageId = 0)
        {
            // Get the package and return as JSON, or return a JSON object with an error message
            Package pkg;
            if (packageId == 0 || 
                (pkg = PackageManager.GetPackageById(packageId)) == null
                )
                return new { errorMessage = "Unable to retrieve package." };

            
            return pkg;
        }

        /// <summary>
        /// An API endpoint for retrieving photos of a specific vacation package
        /// </summary>
        /// <param name="packageId">The packageId of the package to retrieve the photos for</param>
        /// <returns>A string list containing the URLs of the photos</returns>
        [Route("/api/package/gallery/{packageId?}")]
        public string[] GetPackagePhotos(int packageId)
        {
            // Get the folder of images for the selected package
            string path = $@"{Directory.GetCurrentDirectory()}\wwwroot\media\images\vacations\{packageId}";

            // If the folder exists, get all the filenames and generate a list of URLs
            if (Directory.Exists(path))
            {
                return Directory.EnumerateFiles(path)
                    .Select(p => $"/media/images/vacations/{packageId}/{Path.GetFileName(p)}")
                    .ToArray();
            }

            // return the URLs
            return new string[] { };
        }

        /// <summary>
        /// An API endpoint that retrieves the Products that are part of this package, and their supplier
        /// </summary>
        /// <param name="packageId">The id of the package to retrieve info about</param>
        /// <returns>A view model list of PackageProductsSuppliersView</returns>
        [Route("/api/package/products/{packageId?}")]
        public List<PackageProductsSuppliersView> GetPackageProductsSuppliers(int packageId)
        {
            // Get the package by ID
            TravelExpertsContext ctx = new TravelExpertsContext();
            Package pkg = PackageManager.GetPackageById(packageId, ctx);

            // Get the productssuppliers for this package
            List<ProductsSupplier> productsSuppliers = PackageProductSuppliersManager
                .GetProductSuppliers(pkg, ctx);

            // Create a view out of the combined information
            List<PackageProductsSuppliersView> model = new List<PackageProductsSuppliersView>();
            productsSuppliers.ForEach(ps =>
            {
                Product p = ProductManager.GetProduct(ps);
                Supplier s = SupplierManager.GetSupplier(ps);
                model.Add(new PackageProductsSuppliersView
                {
                    ProductName = p.ProdName,
                    SupplierName = s.SupName
                });
            });

            // Return the model, ordered by product name
            return model.OrderBy(m => m.ProductName).ToList();
        }

        /// <summary>
        /// Handles the customer view bookings page
        /// </summary>
        /// <param name="id">The ID of the customer</param>
        /// <returns>A view showing the customer their bookings</returns>
        // GET: /Booking/4
        [HttpGet]
        [Route("{controller}/{action}/{id}")]
        public ActionResult ViewBookings(int id)
        {
            // Get the logged in customer, or id 0 if there isn't one
            int loggedInId = HttpContext.Session.GetInt32("CurrentCustomer") ?? 0;

            // Redirect to login page if there is no customer logged in
            if (loggedInId == 0)
            {
                HttpContext.Session.SetObject<string>("LoginErrorMessage", "You must log in to view your bookings.");
                return RedirectToAction("Login", "Account");
            }

            // Send the customer ID to the next page
            ViewBag.CustomerId = loggedInId;
            
            TravelExpertsContext db = new TravelExpertsContext();   // db connection
            
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

            // Show the most recent bookings first
            model.BookingGroup = model.BookingGroup.OrderByDescending(bg => bg.BookingDate).ToList();

            // Return the view and model
            return View(model);
        }

        /// <summary>
        /// Returns a view page for the user to select additional trip options, like the number of
        /// travelers and the trip type
        /// </summary>
        /// <param name="packageId">The id of the package selected by the user</param>
        /// <returns>The booking options view</returns>
        [HttpGet]
        public ActionResult Options(int packageId)
        {
            // Persist the data need for the options page
            TempData["packageId"] = packageId;
            ViewBag.SelectNumTravelers = Utils.SelectRange(1, 10);
            List<TripType> tripTypes = TripTypeManager.GetTripTypes();
            ViewBag.SelectTripClass = new SelectList(tripTypes, "TripTypeId", "Ttname");
            ViewBag.PackageName = PackageManager.GetPackageName(packageId);

            // Return the options page view
            return View();
        }

        /// <summary>
        /// This handles the actual booking of the items in the cart
        /// </summary>
        /// <returns>
        ///     Either a thank you page, or redirect to login (if unauthorized), or redirect
        ///     to the home page if there's nothing in the cart
        /// </returns>
        [Authorize]
        [HttpGet]
        public ActionResult Checkout()
        {
            // Get the logged in customer id
            string id = "";
            HttpContext.Request.Cookies.TryGetValue("CustomerId", out id);

            int customerId = int.Parse(id);

            //This check should now be redundant due to Authorize -Alex
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

            // Return the thank you page
            return View();
        }

        /// <summary>
        /// Generates a finalization page, where the user can review the booking before adding it to the cart
        /// </summary>
        /// <param name="packageId">The id of the package being added to the cart</param>
        /// <param name="numTravelers">The number of people traveling</param>
        /// <param name="tripTypeId">The trip type</param>
        /// <returns>A view showing the user their booking info</returns>
        [HttpPost]
        public ActionResult FinalizeOptions(int packageId, int numTravelers, string tripTypeId)
        {
            // Generate the view for the user to see their booking info before they confirm
            CartItemViewModel cartItemView = CartItemViewModel.BuildCartItem(packageId, numTravelers, tripTypeId);
            DateTime startDate = (DateTime)cartItemView.Package.PkgStartDate;
            DateTime endDate = (DateTime)cartItemView.Package.PkgEndDate;
            ViewBag.StartDate = startDate.ToString("MMMM dd, yyyy");
            ViewBag.EndDate = endDate.ToString("MMMM dd, yyyy");

            return View(cartItemView);
        }
    }
}
