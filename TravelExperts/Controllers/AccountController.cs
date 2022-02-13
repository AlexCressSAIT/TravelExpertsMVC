using DataManagerAPI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;

namespace TravelExperts.Controllers
{
    public class AccountController : Controller
    {
        // Serves the register view
        public IActionResult Register()
        {
            return View();
        }

        // Handles the post from the register view, add the filled information to database
        // Creates user
        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            try
            {
                CustomerManager.AddCustomer(customer);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
            
        }
    }
}
