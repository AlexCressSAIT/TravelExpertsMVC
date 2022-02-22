using DataManagerAPI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelExpertsData;

/*
 * Controller that holds all Actions for login and register system
 * Author : Daniel Palmer
 * Date: 2022-02-05
 * 
 * Updated by: Alex Cress -Various changes throughout
 */

namespace TravelExperts.Controllers
{
    public class AccountController : Controller
    {
        // Action returns the Login View
        // Author : Daniel Palmer
        public IActionResult Login(string ReturnUrl = "")
        {
            if (ReturnUrl != null)
            {
                TempData["returnUrl"] = ReturnUrl;
            }
            return View();
        }
        // Login Post method that handles Authenticate passed account values
        // Stores Authenticated account in the session and cookies
        // Author : Daniel Palmer
        // Updated By: Alex Cress -added handling for invalid username/password
        //                        -added cookie functionality
        [HttpPost]
        public async Task<IActionResult> Login(Customer customer)
        {
            // Checks that inputs are filled
            if (ModelState["CustUsername"].Errors.Count > 0 
                || ModelState["CustPassword"].Errors.Count > 0)
            {
                ViewBag.LoginError = "Please fill out all fields.";
                return View();
            }
            // Authenticates account values
            Customer loginCustomer = CustomerManager.Authenticate(customer.CustUsername, customer.CustPassword);
            if (loginCustomer == null)
            {
                ViewBag.LoginError = "The username and password do not match.";
                return View();
            }
            
            // Loads Authenticated CustomerId into session
            HttpContext.Session.SetInt32("CurrentCustomer", loginCustomer.CustomerId);

            // Creates a claim of customer name
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{loginCustomer.CustFirstName} {loginCustomer.CustLastName}"),
                new Claim("UserName", loginCustomer.CustUsername),

            };

            // Adds claim to cookies
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("Cookies", principal);

            //Set cookies
            HttpContext.Response.Cookies.Append("CustomerId", loginCustomer.CustomerId.ToString());

            // Redirects to value stored in the TepData["ReturnUrl]
            // Redirects Home if the value does not exist
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

        // Author: Alex Cress
        /// <summary>
        /// Logs out a customer and expires their cookie
        /// </summary>
        /// <returns>redirects to home</returns>
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
        //Updated by: Alex Cress -Fixed bug with login
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(Customer customer)
        {

                try
                {
                CustomerManager.VerifyUsername(customer.CustUsername);
                    Customer newCustomer = CustomerManager.AddCustomer(customer);
                    await Login(customer);
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    ViewBag.UserNameError = "Username is already taken";
                    return View();
                }
            
        }
    }
}