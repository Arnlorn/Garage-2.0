using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Garage_2._0.Models
{
    public class RecieptViewModel
    {
        [Display(Name = "Transaction ID")]
        public int Id { get; set; }

        [Display(Name = "Type of vehicle")]
        public Types Type { get; set; }

        [Display(Name = "Registration number")]
        public string RegNr { get; set; }

        [Display(Name = "Check-in  time")]
        public DateTime TimeStamp { get; set; }

        [Display(Name = "Check-out time")]
        public DateTime Now => DateTime.Now;

        [Display(Name = "Parked for")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm\\:ss}")]
        public TimeSpan Duration => DateTime.Now - TimeStamp;

        [Display(Name = "Price per minute")]
        public decimal PricePerMinute => 5; //Hardcoded price makes for frequent code updates ... more billable hours, wohooooo ;)

        [Display(Name = "Cost")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public decimal Cost => (decimal)(int)(Duration.TotalMinutes + 1 ) * PricePerMinute;

    }
}

