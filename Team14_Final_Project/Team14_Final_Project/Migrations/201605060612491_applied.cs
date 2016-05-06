namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applied : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Positions", "ApplyPosition_AppliedID", "dbo.Applieds");
            DropForeignKey("dbo.Students", "ApplyStudent_AppliedID", "dbo.Applieds");
            DropIndex("dbo.Positions", new[] { "ApplyPosition_AppliedID" });
            DropIndex("dbo.Students", new[] { "ApplyStudent_AppliedID" });
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        ApplicationID = c.Int(nullable: false, identity: true),
                        ApplicationStatus = c.Int(nullable: false),
                        StudentApplied_StudentID = c.Int(),
                        Positionspplied_PositionID = c.Int(),
                    })
                .PrimaryKey(t => t.ApplicationID)
                .ForeignKey("dbo.Students", t => t.StudentApplied_StudentID)
                .ForeignKey("dbo.Positions", t => t.Positionspplied_PositionID)
                .Index(t => t.StudentApplied_StudentID)
                .Index(t => t.Positionspplied_PositionID);
            
            //DropColumn("dbo.Positions", "ApplyPosition_AppliedID");
            //DropColumn("dbo.Students", "ApplyStudent_AppliedID");
            //DropTable("dbo.Applieds");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Applieds",
                c => new
                    {
                        AppliedID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.AppliedID);
            
            AddColumn("dbo.Students", "ApplyStudent_AppliedID", c => c.Int());
            AddColumn("dbo.Positions", "ApplyPosition_AppliedID", c => c.Int());
            DropForeignKey("dbo.Applications", "Positionspplied_PositionID", "dbo.Positions");
            DropForeignKey("dbo.Applications", "StudentApplied_StudentID", "dbo.Students");
            DropIndex("dbo.Applications", new[] { "Positionspplied_PositionID" });
            DropIndex("dbo.Applications", new[] { "StudentApplied_StudentID" });
            DropTable("dbo.Applications");
            CreateIndex("dbo.Students", "ApplyStudent_AppliedID");
            CreateIndex("dbo.Positions", "ApplyPosition_AppliedID");
            AddForeignKey("dbo.Students", "ApplyStudent_AppliedID", "dbo.Applieds", "AppliedID");
            AddForeignKey("dbo.Positions", "ApplyPosition_AppliedID", "dbo.Applieds", "AppliedID");
        }
    }
}
