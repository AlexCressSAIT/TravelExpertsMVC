using DataManagerAPI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace TravelExperts.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        
        [Route("[controller]/CustomerProfile/{id?}")]
        public IActionResult CustomerProfile()
        {
            string id = null;
            Request.Cookies.TryGetValue("CustomerId", out id);
            HttpContext.Session.SetInt32("CurrentCustomer", int.Parse(id));

            Customer customer = CustomerManager.GetCustomer(Convert.ToInt32(id));
            return View(customer);
        }

        public IActionResult EditProfile()
        {
            ViewBag.IsEdit = true;
            string id = null;
            Request.Cookies.TryGetValue("CustomerId", out id);
            HttpContext.Session.SetInt32("CurrentCustomer", int.Parse(id));
            Customer customer = CustomerManager.GetCustomer(int.Parse(id));
            return View("~/Views/Profile/CustomerProfile.cshtml", customer);
        }

        [HttpPost]
        public IActionResult SaveProfile(Customer customer)
        {
            CustomerManager.UpdateCustomer(customer);

            return RedirectToAction("CustomerProfile", "Profile");
        }
    }
}
