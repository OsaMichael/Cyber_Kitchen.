namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatId = c.Int(nullable: false, identity: true),
                        VoterId = c.Int(),
                        RestId = c.Int(),
                        Taste = c.Int(nullable: false),
                        Quality = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TimeLiness = c.Int(nullable: false),
                        CustomerServices = c.Int(nullable: false),
                        TotalScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RatId)
                .ForeignKey("dbo.Restaurants", t => t.RestId)
                .ForeignKey("dbo.Voters", t => t.VoterId)
                .Index(t => t.VoterId)
                .Index(t => t.RestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "VoterId", "dbo.Voters");
            DropForeignKey("dbo.Ratings", "RestId", "dbo.Restaurants");
            DropIndex("dbo.Ratings", new[] { "RestId" });
            DropIndex("dbo.Ratings", new[] { "VoterId" });
            DropTable("dbo.Ratings");
        }
    }
}
