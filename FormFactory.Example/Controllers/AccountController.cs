using System;
using System.Collections.Generic;
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
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: display success / log in
                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            return View();
        }
    }
}
