using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FormFactory.Example.Models;

namespace FormFactory.Example.Controllers
{
    public class AccountController : Controller
    {
        private static AccountModel _storedAccountModel =
            new AccountModel
                {
                    Email = "john.smith@example.com",
                    Title = "Mr",
                    FirstName = "John",
                    LastName = "Smith",
                    Organisation = "Example.com"
                };

        [HttpGet]
        public ActionResult Update()
        {
            ViewData.Model = _storedAccountModel;
            return View();
        }

        [HttpPost]
        public ActionResult Update(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                _storedAccountModel = model;
                // report success
                return RedirectToAction("Update");
            }

            return View();
        }


        [HttpGet]
        public ActionResult Register(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Register([NoLabel] RegisterModel model, [DataType("Hidden")] string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // TODO: display success / log in
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            return View();
        }
    }
}
