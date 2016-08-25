using System;
using System.ComponentModel.DataAnnotations;
using FormFactory.Example.Models;
using Microsoft.AspNetCore.Mvc;

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
            var model = new RegisterModel
                            {
                                BritishDate = DateTime.Today, 
                                DefaultDate = DateTime.Today
                            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Register( RegisterModel model, [DataType("Hidden")] string returnUrl)
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

        [HttpGet]
        public ActionResult ConflictingModelsTest()
        {
            var viewModel = new ConflictingModel2();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult ConflictingModelsTest( ConflictingModel1 model)
        {
            // for this test, just redisplay form
            return View();
        }




        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult LogIn( LogInModel model, [DataType("Hidden")] string returnUrl)
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
