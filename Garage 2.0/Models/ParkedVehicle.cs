using System;
using System.ComponentModel.DataAnnotations;

namespace Garage_2._0.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }

        [Display(Name = "Type")]
        [Range(1,int.MaxValue,ErrorMessage = "Select vehicle type")]
        public Types Type { get; set; }

        [Display(Name = "Reg Nr")]
        // [Index("IX_RegNum", IsUnique = true)]
        [Required]
        public string RegNr { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Make")]
        public string Make { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Range(0,50, ErrorMessage = "Please input the number of wheels (between zero and fifty)")]
        [Display(Name="Wheels")]
        public int NrOfWheels { get; set; }

        [Display(Name = "Time of parking")]
        public DateTime TimeStamp { get; set; }

        [Display(Name = "Parking Slot")]
        public int ParkingSlot { get; set; }
    }
}