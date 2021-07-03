using CaptchaMvc.HtmlHelpers;
using DestinationViews.Company.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DestinationViews.Company.Controllers
{
    public class ManageController : Controller
    {
        private EmployeeDbContext db = new EmployeeDbContext();

        // GET: Manage/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Manage/Login/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            ViewModel viewModel = new ViewModel();

            if (this.IsCaptchaValid("Validate your captcha"))
            {
                viewModel.user = db.Users.Where(u => u.Email.Equals(user.Email) && u.Password.Equals(user.Password)).FirstOrDefault();
            }
            ViewBag.ErrorMessage1 = "Captcha is not correct. Try again.";
            
            if (viewModel.user != null)
            {
                Session["user"] = viewModel.user;
                return RedirectToAction("Index", "Home", Session["user"]);
            }
            else
            {
                ViewBag.ErrorMessage2 = "* This Email is not registed or Password not correct.";
                return View(viewModel);
            }

        }

        // GET: Manage/logout/5
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        //// POST: /Manage/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //    return RedirectToAction("Index", "Home");
        //}

        // GET: Manage/UserDashBoard

        //public ActionResult Redirect()
        //{
        //    if (Session["IsAdmin"] != null)
        //    {
        //        Boolean State = true;
        //        if (Session["IsAdmin"].Equals(State))
        //        {
        //            return RedirectToAction("", "");
        //        }
        //        else
        //        {
        //            return RedirectToAction("", "");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}
    }
}