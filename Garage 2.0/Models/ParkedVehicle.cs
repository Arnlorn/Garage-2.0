using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Garage_2._0.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }
        public Types Type { get; set; }
       // [Index("IX_RegNum", IsUnique = true)]
        public string RegNr { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int NrOfWheels { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}