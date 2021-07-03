using DestinationViews.Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DestinationViews.Company.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDbContext db = new EmployeeDbContext();

        // Get: Employee/Index
        public ActionResult Index(int uID)
        {
            ViewModel viewModel = new ViewModel();

            viewModel.user = db.Users.Single(u => u.ID == uID);
            viewModel.employee = db.Employees.Single(e => e.UserID == uID);
            viewModel.Requests = db.Requests.Where(r => r.EmployeeID == viewModel.employee.ID).ToList();
            viewModel.EmpVacationTypes = db.EmpVacationTypes.ToList();

            if (TempData["Message"] != null)
            {
                ViewBag.ErrorMessage = "* You are not made any request yet.";
            }

            int Casual = 0;
            int Schedule = 0;

            ViewBag.Casual = Casual;
            ViewBag.Schedule = Schedule;

            if (viewModel.Requests.Count() != 0)
            {
                foreach (Request item in viewModel.Requests)
                {
                    if (item.EmpVacationTypeID.Equals(1))
                    {
                        Casual = Casual + item.NumberOfDays;
                    }
                    else
                    {
                        Schedule = Schedule + item.NumberOfDays;
                    }
                }
                ViewBag.Casual = Casual;
                ViewBag.Schedule = Schedule;
                return View(viewModel);
            }
            else
            {
                return View(viewModel);
            }
        }
        
        // GET: Employee/Details/5
        public ActionResult Details(int uID)
        {
            ViewModel viewModel = new ViewModel();

            int eID = db.Employees.Single(e => e.UserID == uID).ID;
            viewModel.Requests = db.Requests.Where(r => r.EmployeeID == eID).ToList();

            if (viewModel.Requests.Count() != 0)
            {
                foreach (Request item in viewModel.Requests)
                {
                    viewModel.empVacationType = db.EmpVacationTypes.Single(t => t.ID == item.EmpVacationTypeID);
                    viewModel.employee = db.Employees.Single(e => e.ID == eID);
                    viewModel.user = db.Users.Single(u => u.ID == uID);
                }
                return View(viewModel);
            }
            else
            {
                TempData["Message"] = "Activate Error Message.";
                return RedirectToAction("Index", new { uID = uID });
            }
        }

        // GET: Employee/Request
        public ActionResult NewRequest(int uID)
        {
            ViewModel viewModel = new ViewModel();

            viewModel.employee = db.Employees.Single(e => e.UserID == uID);
            viewModel.request = new Request();
            viewModel.request.EmployeeID = viewModel.employee.ID;

            return View(viewModel);
        }

        // POST: Employee/Request
        [HttpPost]
        public ActionResult NewRequest(int uID, ViewModel viewModel1)
        {
            try
            {
                ViewModel viewModel2 = new ViewModel();
                viewModel2.request = new Request();

                viewModel2.request.ID = viewModel1.request.ID;
                viewModel2.request.EmployeeID = viewModel1.request.EmployeeID;
                if (viewModel1.empVacationType.TypeName.ToString() == "Casual")
                {
                    viewModel2.request.EmpVacationTypeID = 1;
                }
                else
                {
                    viewModel2.request.EmpVacationTypeID = 2;
                }
                viewModel2.request.NumberOfDays = viewModel1.request.NumberOfDays;
                viewModel2.request.StartDate = viewModel1.request.StartDate;
                viewModel2.request.EndDate = viewModel1.request.EndDate;
                viewModel2.request.Accepted = viewModel1.request.Accepted;
                db.Requests.Add(viewModel2.request);
                db.SaveChanges();

                return RedirectToAction("Index", new { uID = uID });
            }
            catch
            {
                return View();
            }
        }
    }
}