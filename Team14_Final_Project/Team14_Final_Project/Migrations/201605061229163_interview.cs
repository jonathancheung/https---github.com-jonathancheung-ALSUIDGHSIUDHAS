namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class interview : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Interviews",
                c => new
                    {
                        InterviewID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InterviewID)
                .ForeignKey("dbo.Applications", t => t.InterviewID)
                .Index(t => t.InterviewID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interviews", "InterviewID", "dbo.Applications");
            DropIndex("dbo.Interviews", new[] { "InterviewID" });
            DropTable("dbo.Interviews");
        }
    }
}
