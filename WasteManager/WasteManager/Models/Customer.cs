using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WasteManager.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Street Address")]
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        [Display(Name = "City, State")]
        public string CityState { get; set; }
        [Required]
        [Display(Name = "Zipcode")]
        public int zip { get; set; }
        [Required]
        [Display(Name = "Pickup Day")]
        public DayOfWeek? WeeklyPickupDay { get; set; }

        [Display(Name = "Monthly Extra Date")]
        public DateTime? MonthlyExtraDate { get; set; }

        [Display(Name = "Pause Service Date")]
        public DateTime? PickupPause { get; set; }

        [Display(Name = "Resume Service Date")]
        public DateTime? PickupResume { get; set; }
        public double? Balance { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        [NotMapped]
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
        //consider parameters for javascript maps api

        //payment information

    }
}
