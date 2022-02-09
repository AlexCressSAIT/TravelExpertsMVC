using DataManagerAPI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;

namespace TravelExperts.Controllers
{
    public class ProfileController : Controller
    {
        [Route("[controller]/CustomerProfile/{id?}")]
        public IActionResult CustomerProfile(string id = "")
        {
            if (id == "")
            {
                return RedirectToAction("Index", "Home");
            }
            Customer customer = CustomerManager.GetCustomer(Convert.ToInt32(id));
            return View(customer);
        }
    }
}
