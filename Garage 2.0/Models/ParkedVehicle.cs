using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Garage_2._0.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }

        [Display(Name = "Type of vehicle")]
        public Types Type { get; set; }

        [Display(Name = "Registration number")]
        // [Index("IX_RegNum", IsUnique = true)]
        public string RegNr { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Make")]
        public string Make { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name="Number of wheels")]
        public int NrOfWheels { get; set; }

        [Display(Name = "Time of parking")]
        public DateTime TimeStamp { get; set; }
    }
}