using DestinationViews.Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewModel viewModel = new ViewModel();

            if (Session["user"] != null)
            {
                viewModel.user = Session["user"] as User;
            }

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Comming Soon";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Comming Soon";

            return View();
        }
    }
}