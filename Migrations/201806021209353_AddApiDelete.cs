namespace Cyber_Kitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApiDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "IsCanceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "IsCanceled");
        }
    }
}
