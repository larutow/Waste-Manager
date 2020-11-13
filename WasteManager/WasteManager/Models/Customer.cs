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
        public string StreetAddress { get; set; }
        public string CityState { get; set; }
        public int zip { get; set; }
        public DayOfWeek WeeklyPickupDay { get; set; }
        public DateTime MonthlyExtraDate { get; set; }
        public DateTime PickupPause { get; set; }
        public DateTime PickupResume { get; set; }
        public double Balance { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        //consider parameters for javascript maps api

        //payment information

    }
}
