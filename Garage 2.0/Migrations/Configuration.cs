namespace Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Garage_2._0.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage_2._0.DataAccessLayer.RegisterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Garage_2._0.DataAccessLayer.RegisterContext context)
        {
            context.parkedVehicles.AddOrUpdate(r => r.RegNr,
                new ParkedVehicle
                {
                    Type = Types.Flygplan,
                    RegNr = "ABC123",
                    Color = "Blue",
                    Make = "Cessna",
                    Model = "A307",
                    NrOfWheels = 3,
                    TimeStamp = DateTime.Now
                },
               new ParkedVehicle
               {
                   Type = Types.Motorcyckel,
                   RegNr = "AFK365",
                   Color = "Red",
                   Make = "Kawasaki",
                   Model = "125:a",
                   NrOfWheels = 2,
                   TimeStamp = DateTime.Now
               },
              new ParkedVehicle
              {
                  Type = Types.Motorcyckel,
                  RegNr = "AFK001",
                  Color = "Black",
                  Make = "Harley Davidsson",
                  Model = "Iron 883",
                  NrOfWheels = 2,
                  TimeStamp = DateTime.Now
              },
              new ParkedVehicle
              {
                  Type = Types.Motorcyckel,
                  RegNr = "HLP356",
                  Color = "Green",
                  Make = "Yamaha",
                  Model = "R15B3",
                  NrOfWheels = 2,
                  TimeStamp = DateTime.Now
              });
        }
    }
}
