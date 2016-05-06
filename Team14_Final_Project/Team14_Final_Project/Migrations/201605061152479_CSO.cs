namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CSO : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CSOes",
                c => new
                    {
                        CSOID = c.Int(nullable: false, identity: true),
                        AppUsers_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CSOID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUsers_Id)
                .Index(t => t.AppUsers_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CSOes", "AppUsers_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CSOes", new[] { "AppUsers_Id" });
            DropTable("dbo.CSOes");
        }
    }
}
