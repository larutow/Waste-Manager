using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using WasteManager.Data;
using WasteManager.Models;
using Customer = WasteManager.Models.Customer;

namespace WasteManager.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
            StripeConfiguration.ApiKey = APIkeys.StripeSK;
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

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession()
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                      UnitAmount = 2000,
                      Currency = "usd",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = "T-shirt",
                      },

                    },
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Json(new { id = session.Id });
        }


        // GET CustomerController/PickupServices
        public ActionResult PickupServices(int id)
        {
            var cust = _context.Customers.Where(i => i.Id == id).FirstOrDefault();
            CustomerDaysViewModel custDays = new CustomerDaysViewModel();
            custDays.Customer = cust;
            return View(custDays);
        }

        // POST: CustomerController/PickupServices
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PickupServices(CustomerDaysViewModel viewmodel)
        {
            var thisCust = _context.Customers.Where(i => i.Id == viewmodel.Customer.Id).FirstOrDefault();
            thisCust.WeeklyPickupDay = viewmodel.Customer.WeeklyPickupDay;
            thisCust.MonthlyExtraDate = viewmodel.Customer.MonthlyExtraDate;
            thisCust.PickupPause = viewmodel.Customer.PickupPause;
            thisCust.PickupResume = viewmodel.Customer.PickupResume;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            var newCustomer = new Customer();
            CustomerDaysViewModel custDays = new CustomerDaysViewModel();
            custDays.Customer = newCustomer;
            return View(custDays);
        }

        
        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                
                string address = $"{customer.StreetAddress},{customer.CityState},{customer.Zip}";
                string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={APIkeys.Mapskey}";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    LocationData custLocationData = JsonConvert.DeserializeObject<LocationData>(jsonResult);
                    if(custLocationData.results[0].geometry.location_type == "ROOFTOP")
                    {
                        customer.Lat = custLocationData.results[0].geometry.location.lat;
                        customer.Lng = custLocationData.results[0].geometry.location.lng;
                    }
                }

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
            CustomerDaysViewModel custDays = new CustomerDaysViewModel();
            custDays.Customer = customer;
            return View(custDays);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Customer customer)
        {
            try
            {
                _context.Update(customer);

                string address = $"{customer.StreetAddress},{customer.CityState},{customer.Zip}";
                string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={APIkeys.Mapskey}";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    LocationData custLocationData = JsonConvert.DeserializeObject<LocationData>(jsonResult);
                    if (custLocationData.results[0].geometry.location_type == "ROOFTOP")
                    {
                        customer.Lat = custLocationData.results[0].geometry.location.lat;
                        customer.Lng = custLocationData.results[0].geometry.location.lng;
                    }
                }
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
