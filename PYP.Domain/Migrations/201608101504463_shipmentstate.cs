namespace PYP.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipmentstate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShipmentStates",
                c => new
                    {
                        Key = c.Guid(nullable: false),
                        ShipmentKey = c.Guid(nullable: false),
                        ShipmentStatus = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.Shipments", t => t.ShipmentKey, cascadeDelete: true)
                .Index(t => t.ShipmentKey);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShipmentStates", "ShipmentKey", "dbo.Shipments");
            DropIndex("dbo.ShipmentStates", new[] { "ShipmentKey" });
            DropTable("dbo.ShipmentStates");
        }
    }
}
