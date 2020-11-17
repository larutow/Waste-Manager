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
        public ActionResult Index()
        {
            //identify userId
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = _context.Customers.Where(i => i.IdentityUserId == userId).FirstOrDefault();
            if (employee == null)
            {
                return RedirectToAction("Create");
            }

            return RedirectToAction("DailyList");
        }

        // GET: EmployeesController/CustomerList
        public ActionResult DailyList(DayOfWeek dateselected)
        {
            var thisEmployee = _context.Employees.Where(e => e.IdentityUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();

            //var customersToDisplay = _context.Customers.Where(c => c.WeeklyPickupDay == DateTime.Today.DayOfWeek && c.zip == employee.zip);

            int todayOrSelected = (int?)dateselected ?? (int)DateTime.Today.DayOfWeek;
            DayOfWeek filter = (DayOfWeek)todayOrSelected;


            return View();
        }

        // GET: EmployeesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
