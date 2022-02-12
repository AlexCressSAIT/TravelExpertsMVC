using DataManagerAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: BookingController/Details/5
        public ActionResult ViewBookings(int id)
        {
            ViewBag.Customer = CustomerManager.GetCustomer(id);
            List<Booking> bookings = BookingManager.GetCustomerBookings(id).OrderByDescending(b => b.BookingDate).ToList();
            return View(bookings);
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
