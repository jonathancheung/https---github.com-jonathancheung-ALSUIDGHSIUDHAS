namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addposition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PositionMajors",
                c => new
                    {
                        Position_PositionID = c.Int(nullable: false),
                        Major_MajorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Position_PositionID, t.Major_MajorID })
                .ForeignKey("dbo.Positions", t => t.Position_PositionID, cascadeDelete: true)
                .ForeignKey("dbo.Majors", t => t.Major_MajorID, cascadeDelete: true)
                .Index(t => t.Position_PositionID)
                .Index(t => t.Major_MajorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PositionMajors", "Major_MajorID", "dbo.Majors");
            DropForeignKey("dbo.PositionMajors", "Position_PositionID", "dbo.Positions");
            DropIndex("dbo.PositionMajors", new[] { "Major_MajorID" });
            DropIndex("dbo.PositionMajors", new[] { "Position_PositionID" });
            DropTable("dbo.PositionMajors");
        }
    }
}
