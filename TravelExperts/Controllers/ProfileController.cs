using DataManagerAPI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;
using Microsoft.AspNetCore.Http;


namespace TravelExperts.Controllers
{
    public class ProfileController : Controller
    {
        
        [Route("[controller]/CustomerProfile/{id?}")]
        public IActionResult CustomerProfile()
        {
            if (!Request.Cookies.Keys.Contains("CustomerId"))
            {
                return RedirectToAction("Index", "Home");
            }
            //Security risk as anyone can just set their cookie to anything?
            string id = null;
            Request.Cookies.TryGetValue("CustomerId", out id);
            HttpContext.Session.SetInt32("CurrentCustomer", int.Parse(id));

            Customer customer = CustomerManager.GetCustomer(Convert.ToInt32(id));
            return View(customer);
        }

        public IActionResult EditProfile()
        {
            ViewBag.IsEdit = true;
            Customer customer = CustomerManager.GetCustomer((int)HttpContext.Session.GetInt32("CurrentCustomer"));
            return View("~/Views/Profile/CustomerProfile.cshtml", customer);
        }

        [HttpPost]
        public IActionResult SaveProfile(Customer customer)
        {
            CustomerManager.UpdateCustomer(customer);

            return RedirectToAction("Index", "Home");
        }
    }
}
