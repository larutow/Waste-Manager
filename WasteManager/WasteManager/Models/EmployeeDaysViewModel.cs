using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasteManager.Models
{
    public class EmployeeDaysViewModel
    {
        public List<Customer> Customers { get; set; }

        public int? DayFilter { get; set; }

        public List<SelectListItem> DaysAvailable { get; set; } = new List<SelectListItem>
        {
            new SelectListItem {Value = "0", Text = "Sunday"},
            new SelectListItem {Value = "1", Text = "Monday"},
            new SelectListItem {Value = "2", Text = "Tuesday"},
            new SelectListItem {Value = "3", Text = "Wednesday"},
            new SelectListItem {Value = "4", Text = "Thursday"},
            new SelectListItem {Value = "5", Text = "Friday"},
            new SelectListItem {Value = "6", Text = "Saturday"},
        };

    }
}
