using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        [HttpGet, Authorize]
        public ActionResult Update()
        {
            ViewData.Model = _storedAccountModel;
            return View();
        }

        [HttpPost, Authorize]
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

    }
}
