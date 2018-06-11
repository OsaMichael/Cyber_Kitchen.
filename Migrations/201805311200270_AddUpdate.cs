namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Scores", "RestId", "dbo.Restaurants");
            DropForeignKey("dbo.Scores", "VoterId", "dbo.Voters");
            DropIndex("dbo.Scores", new[] { "VoterId" });
            DropIndex("dbo.Scores", new[] { "RestId" });
            AlterColumn("dbo.Scores", "VoterId", c => c.Int());
            AlterColumn("dbo.Scores", "RestId", c => c.Int());
            CreateIndex("dbo.Scores", "VoterId");
            CreateIndex("dbo.Scores", "RestId");
            AddForeignKey("dbo.Scores", "RestId", "dbo.Restaurants", "RestId");
            AddForeignKey("dbo.Scores", "VoterId", "dbo.Voters", "VoterId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scores", "VoterId", "dbo.Voters");
            DropForeignKey("dbo.Scores", "RestId", "dbo.Restaurants");
            DropIndex("dbo.Scores", new[] { "RestId" });
            DropIndex("dbo.Scores", new[] { "VoterId" });
            AlterColumn("dbo.Scores", "RestId", c => c.Int(nullable: false));
            AlterColumn("dbo.Scores", "VoterId", c => c.Int(nullable: false));
            CreateIndex("dbo.Scores", "RestId");
            CreateIndex("dbo.Scores", "VoterId");
            AddForeignKey("dbo.Scores", "VoterId", "dbo.Voters", "VoterId", cascadeDelete: true);
            AddForeignKey("dbo.Scores", "RestId", "dbo.Restaurants", "RestId", cascadeDelete: true);
        }
    }
}
