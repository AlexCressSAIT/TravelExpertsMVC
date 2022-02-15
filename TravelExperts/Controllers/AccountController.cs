using DataManagerAPI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelExpertsData;

namespace TravelExperts.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Login(string ReturnUrl = "")
        {
            if (ReturnUrl != null)
            {
                TempData["returnUrl"] = ReturnUrl;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Customer customer)
        {
            Customer loginCustomer = CustomerManager.Authenticate(customer.CustUsername, customer.CustPassword);
            if (loginCustomer == null)
            {
                ViewBag.LoginError = "The username and password do not match.";
                return View();
            }

            HttpContext.Session.SetInt32("CurrentCustomer", loginCustomer.CustomerId);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{loginCustomer.CustFirstName} {loginCustomer.CustLastName}"),
                new Claim("UserName", loginCustomer.CustUsername),

            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("Cookies", principal);

            //Set cookies
            HttpContext.Response.Cookies.Append("CustomerId", loginCustomer.CustomerId.ToString());

            string returnUrl = TempData["ReturnUrl"] != null ? TempData["ReturnUrl"].ToString() : null;
            if (String.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect(returnUrl);
            }
        }
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            HttpContext.Session.SetInt32("CurrentCustomer", 0);
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("CustomerId"); //Expire cookie
            return RedirectToAction("Index", "Home");
        }

        // Serves the register view
        public IActionResult Register()
        {
            return View();
        }

        // Handles the post from the register view, add the filled information to database
        // Creates user
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(Customer customer)
        {
            try
            {
                Customer newCustomer = CustomerManager.AddCustomer(customer);
                await Login(customer);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
            
        }
    }
}