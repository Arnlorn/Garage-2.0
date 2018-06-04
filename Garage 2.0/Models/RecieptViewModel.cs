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
        // [Index("IX_RegNum", IsUnique = true)]
        public string RegNr { get; set; }

        [Display(Name = "Time of parking")]
        public DateTime TimeStamp { get; set; }

        public TimeSpan Duration => DateTime.Now - TimeStamp;
        public const decimal PricePerMinute = 5; //Hardcoded price makes for frequent code updates ... more billable hours, wohooooo ;)

    }
}

