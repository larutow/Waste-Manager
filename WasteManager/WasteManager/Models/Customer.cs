using Microsoft.AspNetCore.Identity;
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
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "City, State")]
        public string CityState { get; set; }
        [Display(Name = "Zipcode")]
        public int zip { get; set; }
        [Display(Name = "Pickup Day")]
        public DayOfWeek WeeklyPickupDay { get; set; }
        [Display(Name = "Monthly Extra Date")]
        public DateTime MonthlyExtraDate { get; set; }
        [Display(Name = "Pause Service Date")]
        public DateTime PickupPause { get; set; }
        [Display(Name = "Resume Service Date")]
        public DateTime PickupResume { get; set; }
        public double Balance { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        //consider parameters for javascript maps api

        //payment information

    }
}
