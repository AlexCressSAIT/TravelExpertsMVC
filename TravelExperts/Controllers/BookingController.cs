using DataManagerAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
