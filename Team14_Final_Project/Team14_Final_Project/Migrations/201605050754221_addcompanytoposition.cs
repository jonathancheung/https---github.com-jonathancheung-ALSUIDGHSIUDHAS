namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcompanytoposition : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PositionMajors", newName: "MajorPositions");
            DropPrimaryKey("dbo.MajorPositions");
            AddColumn("dbo.Positions", "CompanyName_CompanyID", c => c.Int());
            AddPrimaryKey("dbo.MajorPositions", new[] { "Major_MajorID", "Position_PositionID" });
            CreateIndex("dbo.Positions", "CompanyName_CompanyID");
            AddForeignKey("dbo.Positions", "CompanyName_CompanyID", "dbo.Companies", "CompanyID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Positions", "CompanyName_CompanyID", "dbo.Companies");
            DropIndex("dbo.Positions", new[] { "CompanyName_CompanyID" });
            DropPrimaryKey("dbo.MajorPositions");
            DropColumn("dbo.Positions", "CompanyName_CompanyID");
            AddPrimaryKey("dbo.MajorPositions", new[] { "Position_PositionID", "Major_MajorID" });
            RenameTable(name: "dbo.MajorPositions", newName: "PositionMajors");
        }
    }
}
