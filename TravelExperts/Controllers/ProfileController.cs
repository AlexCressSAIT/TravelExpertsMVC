using DataManagerAPI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;


/*
 *Controller that holds actions for CustomerProfile View, Login Authorized
 *Author: Daniel Palmer
 *Date: 2022 - 02 - 12
 *Updated by: Alex Cress -Added cookie functionality throughout the class
 */


namespace TravelExperts.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // Action that serves the CustomerProfile View based on Id saved in cookie or session
        [Route("[controller]/CustomerProfile/{id?}")]
        public IActionResult CustomerProfile()
        {
            string id = null;
            Request.Cookies.TryGetValue("CustomerId", out id);
            HttpContext.Session.SetInt32("CurrentCustomer", int.Parse(id));

            Customer customer = CustomerManager.GetCustomer(Convert.ToInt32(id));
            return View(customer);
        }

        // Author: Alex Cress
        /// <summary>
        /// Reloads the Profile page in edit mode.
        /// </summary>
        /// <returns>reloads CustomerProfile page</returns>
        public IActionResult EditProfile()
        {
            //Set cookie and session variables
            ViewBag.IsEdit = true; //Signifies that the page should be loaded in edit mode
            string id = null;
            Request.Cookies.TryGetValue("CustomerId", out id);
            HttpContext.Session.SetInt32("CurrentCustomer", int.Parse(id));

            //Get customer form database
            Customer customer = CustomerManager.GetCustomer(int.Parse(id));

            //Return the CustomerProfile page with the given customer
            return View("~/Views/Profile/CustomerProfile.cshtml", customer);
        }

        // Author: Alex Cress
        /// <summary>
        /// Saves the customer profile
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>redirects to CustomerProfile/Profile</returns>
        [HttpPost]
        public IActionResult SaveProfile(Customer customer)
        {
            CustomerManager.UpdateCustomer(customer);

            return RedirectToAction("CustomerProfile", "Profile");
        }
    }
}
