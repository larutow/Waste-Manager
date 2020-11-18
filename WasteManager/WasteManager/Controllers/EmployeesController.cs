using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WasteManager.Data;
using WasteManager.Models;

namespace WasteManager.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeesController : Controller
    {
        private ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: EmployeesController
        public ActionResult Index(int? dayOfWeekSelected)
        {
            //identify userId
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = _context.Employees.Where(i => i.IdentityUserId == userId).FirstOrDefault();
            if (employee == null)
            {
                return RedirectToAction("Create");
            }

            int dayVal = dayOfWeekSelected ?? (int)DateTime.Today.DayOfWeek;
            DateTime weekStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            DateTime dateFilter = weekStart.AddDays(dayVal);
            DayOfWeek dayFilter = (DayOfWeek)dayVal;

            int zipFilter = employee.Zip;

            //Query:
            //Find customers WHERE:
            //customer is in employee zip
            //AND WHERE 
            //  customer pickup is == day filter (today in this method)
            //  OR
            //  customer extradate == date filter (today in this method)
            //AND WHERE
            //  (pickup pause == null) OR (pickup pause > today)
            //   AND
            //  (pickup resume == null) OR (pickup pause <= today)

            var customersToDisplay = _context.Customers.Where(c => (c.Zip == zipFilter) && ((c.WeeklyPickupDay == dayFilter) || (c.MonthlyExtraDate == dateFilter)) && (((c.PickupPause == null) || (c.PickupPause > dateFilter)) && ((c.PickupResume == null) || (c.PickupResume >= dateFilter)))).ToList();

            EmployeeDaysViewModel modelview = new EmployeeDaysViewModel();
            modelview.Customers = customersToDisplay;
            modelview.DayFilter = (int)dayFilter;

            return View(modelview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EmployeeDaysViewModel viewmodel)
        {
            int daySelected = (int)viewmodel.DayFilter;
            return RedirectToAction("Index", new { dayOfWeekSelected = daySelected});
        }

        // GET: EmployeesController/CustomerList
        public ActionResult DailyList()
        {
            Employee thisEmployee = _context.Employees.Where(e => e.IdentityUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();

            DateTime dateFilter = DateTime.Today;
            DayOfWeek dayFilter = dateFilter.DayOfWeek;
            int zipFilter = thisEmployee.Zip;
            
            //Query:
            //Find customers WHERE:
            //customer is in employee zip
            //AND WHERE 
            //  customer pickup is == date filter (today in this method)
            //  OR
            //  customer extradate == date filter (today in this method)
            //AND WHERE
            //  (pickup pause == null) OR (pickup pause > today)
            //   AND
            //  (pickup resume == null) OR (pickup pause <= today)

            var customersToDisplay = _context.Customers.Where(c => (c.Zip == zipFilter) && ((c.WeeklyPickupDay == dayFilter)||(c.MonthlyExtraDate == dateFilter)) && (((c.PickupPause == null)||(c.PickupPause > dateFilter)) && ((c.PickupResume == null)||(c.PickupResume>=dateFilter)))).ToList();
            EmployeeDaysViewModel model = new EmployeeDaysViewModel();
            model.Customers = customersToDisplay;
            

            return View(model);
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            var cust = _context.Customers.Where(c => c.Id == id).FirstOrDefault();
            //probably need a viewmodel
            return View(cust);
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee newemployee)
        {
            try
            {
                _context.Employees.Add(newemployee);
                newemployee.IdentityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.SaveChanges();
                return RedirectToAction("DailyList");
            }
            catch
            {
                return View(newemployee);
            }
        }

        // GET: EmployeesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
