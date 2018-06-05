namespace Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParkingSlot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkedVehicles", "ParkingSlot", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParkedVehicles", "ParkingSlot");
        }
    }
}
