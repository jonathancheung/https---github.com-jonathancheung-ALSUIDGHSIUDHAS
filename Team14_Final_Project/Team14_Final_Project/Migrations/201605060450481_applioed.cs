namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applioed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applieds",
                c => new
                    {
                        AppliedID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppliedID)
                .ForeignKey("dbo.Positions", t => t.AppliedID)
                .ForeignKey("dbo.Students", t => t.AppliedID)
                .Index(t => t.AppliedID);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applieds", "AppliedID", "dbo.Students");
            DropForeignKey("dbo.Applieds", "AppliedID", "dbo.Positions");
            DropIndex("dbo.Applieds", new[] { "AppliedID" });
            DropTable("dbo.Applieds");
        }
    }
}
