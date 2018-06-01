using System;
using System.ComponentModel.DataAnnotations;

namespace Garage_2._0.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }

        [Display(Name = "Type of vehicle")]
        [Range(1,int.MaxValue,ErrorMessage = "Select vehicle type")]
        public Types Type { get; set; }

        [Display(Name = "Registration number")]
        // [Index("IX_RegNum", IsUnique = true)]
        [Required]
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