using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WasteManager.Data;
using WasteManager.Models;

namespace WasteManager.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: CustomersController
        public ActionResult Index()
        {
            //identify userId
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);        
            var cust = _context.Customers.Where(i => i.IdentityUserId == userId).FirstOrDefault();
            if(cust == null)
            {
                return RedirectToAction("Create");
            }
            
            return View(cust);

        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET CustomerController/PickupServices
        public ActionResult PickupServices(int id)
        {
            var cust = _context.Customers.Where(i => i.Id == id).FirstOrDefault();
            return View(cust);
        }

        // POST: CustomerController/PickupServices
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PickupServices(Customer customerDates)
        {
            var thisCust = _context.Customers.Where(i => i.Id == customerDates.Id).FirstOrDefault();
            thisCust.WeeklyPickupDay = customerDates.WeeklyPickupDay;
            thisCust.MonthlyExtraDate = customerDates.MonthlyExtraDate;
            thisCust.PickupPause = customerDates.PickupPause;
            thisCust.PickupResume = customerDates.PickupResume;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            var newCustomer = new Customer();
            return View(newCustomer);
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                customer.IdentityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(customer);
            }
        }
        //edits customer's basic profile (Name/Address/Pickup Day)
        // GET: CustomersController/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.Where(i => i.Id == id).FirstOrDefault();


            return View(customer);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                _context.Update(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(customer);
            }
        }

        // GET: CustomersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomersController/Delete/5
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
