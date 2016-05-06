namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class something : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.AspNetUsers", new[] { "Recruiters_RecruiterID" });
            //RenameColumn(table: "dbo.Recruiters", name: "Recruiters_RecruiterID", newName: "AppUsers_Id");
            AddColumn("dbo.Recruiters", "Recruiters_RecruiterID", c => c.Int());
            AddForeignKey("dbo.Recruiters", "Recruiters_RecruiterID", "dbo.Recruiters", "RecruiterID");
            //CreateIndex("dbo.Recruiters", "AppUsers_Id");
            //DropColumn("dbo.AspNetUsers", "Recruiters_RecruiterID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Recruiters_RecruiterID", c => c.Int());
            DropIndex("dbo.Recruiters", new[] { "AppUsers_Id" });
            RenameColumn(table: "dbo.Recruiters", name: "AppUsers_Id", newName: "Recruiters_RecruiterID");
            CreateIndex("dbo.AspNetUsers", "Recruiters_RecruiterID");
        }
    }
}
