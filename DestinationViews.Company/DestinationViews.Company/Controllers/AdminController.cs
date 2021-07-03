using DestinationViews.Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using System.Dynamic;
using System.Collections;

namespace DestinationViews.Company.Controllers
{
    public class AdminController : Controller
    {
        private EmployeeDbContext db = new EmployeeDbContext();

        // GET: Admin
        public ActionResult List()
        {
            ViewModel viewModel = new ViewModel();

            viewModel.Employees = db.Employees.ToList();

            // Count the number of uncheck requests:
            viewModel.NumbersOfRequsts = new List<int>();
            foreach (Employee employee in viewModel.Employees)
            {
                int id = employee.ID;
                viewModel.Requests = db.Requests.Where(e => e.EmployeeID == id).ToList();


                viewModel.NoOfRequests = 0;
                foreach (Request request in viewModel.Requests)
                {
                    if (request.Accepted == null)
                    {
                        viewModel.NoOfRequests = viewModel.NoOfRequests + 1;
                    }
                }
                
                viewModel.NumbersOfRequsts.Add(viewModel.NoOfRequests);

                viewModel.user = db.Users.Single(e => e.ID == employee.UserID);

                Employee existing = db.Employees.Find(employee.ID);
                existing.user.Full_Name = viewModel.user.Full_Name;
                existing.user.Email = viewModel.user.Email;
                existing.Birthdate = employee.Birthdate;
                existing.Gender = employee.Gender;
            }

            // Redirected form Details action.
            if (TempData["Message"] != null)
            {
                ViewBag.ErrorMessage = "* This employee has not made any request yet.";
            }

            return View(viewModel);
        }

        // GET: Admin/DeleteVacationType
        public ActionResult DeleteVacationType(int id)
        {
            try
            {
                ViewModel viewModel = new ViewModel();

                viewModel.empVacationType = db.EmpVacationTypes.Single(x => x.ID == id);

                return View(viewModel);
            }
            catch
            {
                return View();
            }
        }

        // POST: Admin/DeleteVacationType/5
        [HttpPost]
        public ActionResult DeleteVacationType(int id, FormCollection collection)
        {
            ViewModel viewModel = new ViewModel();

            try
            {
                viewModel.empVacationType = db.EmpVacationTypes.Single(x => x.ID == id);
                db.EmpVacationTypes.Remove(viewModel.empVacationType);
                db.SaveChanges();
                return RedirectToAction("Balance");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Balance
        public ActionResult Balance()
        {
            ViewModel viewModel = new ViewModel();

            viewModel.EmpVacationTypes = db.EmpVacationTypes.ToList();
            return View(viewModel);
        }

        // POST: Admin/Balance/5
        [HttpPost]
        public ActionResult Balance(List<EmpVacationType> EmpVacationTypes)
        {
            ViewModel viewModel = new ViewModel();

            viewModel.EmpVacationTypes = EmpVacationTypes.ToList();
            try
            {
                foreach (EmpVacationType empVacationType in viewModel.EmpVacationTypes)
                {
                    EmpVacationType existing = db.EmpVacationTypes.Find(empVacationType.ID);
                    existing.TypeName = empVacationType.TypeName;
                    existing.DefaultBalance = empVacationType.DefaultBalance;
                }
                db.SaveChanges();
                return View(viewModel);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            ViewModel viewModel = new ViewModel();

            viewModel.Requests = db.Requests.Where(e => e.EmployeeID == id).ToList();

            if (viewModel.Requests.Count() != 0)
            {
                foreach (Request request in viewModel.Requests)
                {
                    viewModel.empVacationType = db.EmpVacationTypes.Single(t => t.ID == request.EmpVacationTypeID);
                    viewModel.employee = db.Employees.Single(u => u.ID == request.EmployeeID);
                    viewModel.user = db.Users.Single(u => u.ID == viewModel.employee.UserID);
                }
                return View(viewModel);
            }
            else
            {
                TempData["Message"] = "Activate Error Message.";
                return RedirectToAction("List");
            }
        }

        // POST: Admin/Details/5
        [HttpPost]
        public ActionResult Details(int id, List<Request> Requests)
        {
            ViewModel viewModel = new ViewModel();

            viewModel.Requests = db.Requests.Where(e => e.EmployeeID == id).ToList();

            foreach (Request request in viewModel.Requests)
            {
                viewModel.empVacationType = db.EmpVacationTypes.Single(t => t.ID == request.EmpVacationTypeID);
                viewModel.employee = db.Employees.Single(u => u.ID == request.EmployeeID);
                viewModel.user = db.Users.Single(u => u.ID == viewModel.employee.UserID);

                try
                {
                    request.Accepted = Requests.Single(r => r.ID == request.ID).Accepted;
                }
                catch
                {
                    continue;
                }
            }
            db.SaveChanges();
            return View(viewModel);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewModel viewModel = new ViewModel();

            return View(viewModel);
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            string FullName = employee.user.Full_Name;
            
            if (ModelState.IsValid)
            {
                try
                {
                    var nameAlreadyExists = db.Users.Single(e => e.Full_Name == FullName);
                    ModelState.AddModelError(string.Empty, "This Employee Name already exist in Data Base.");
                    return View();
                }
                catch
                {
                    if (this.IsCaptchaValid("Validate your captcha"))
                    {
                        db.Employees.Add(employee);
                        db.SaveChanges();
                        return RedirectToAction("List");
                    }
                    ViewBag.ErrorMessage = "Captcha is not correct.";
                    return View();
                }
            }
            else
            {
                if (this.IsCaptchaValid("Validate your captcha"))
                {
                    return View();
                }
                ViewBag.ErrorMessage = "Captcha is not correct.";
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            ViewModel viewModel = new ViewModel();

            viewModel.employee = db.Employees.Single(e => e.UserID == id);
            viewModel.user = db.Users.Single(e => e.ID == id);

            return View(viewModel);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ViewModel viewModel = new ViewModel();

                viewModel.employee = db.Employees.Single(x => x.UserID == id);
                db.Employees.Remove(viewModel.employee);
                viewModel.user = db.Users.Single(u => u.ID == id);
                db.Users.Remove(viewModel.user);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
    }
}
