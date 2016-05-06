namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinterview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "ApplicationTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "ApplicationTitle");
        }
    }
}
