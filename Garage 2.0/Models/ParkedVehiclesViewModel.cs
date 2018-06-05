using System;

namespace Garage_2._0.Models
{
    public class ParkedVehiclesViewModel
    {
        public int Id { get; set; }
        public Types Type { get; set; }
        public string RegNr { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int NrOfWheels { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ParkingSlot { get; set; }
    }
}