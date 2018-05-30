using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garage_2._0.Models;



namespace Garage_2._0.DataAccessLayer
{
    public class RegisterContext : DbContext
    {
        public RegisterContext() : base("GarageAtarkomsten") {}
        public DbSet<ParkedVehicle> parkedVehicles { get; set; }
    }
}