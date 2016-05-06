namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asildhaslu : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Applieds", new[] { "AppliedID" });
            DropPrimaryKey("dbo.Applieds");
            DropTable("dbo.Applieds");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "ApplyStudent_AppliedID", "dbo.Applieds");
            DropForeignKey("dbo.Positions", "ApplyPosition_AppliedID", "dbo.Applieds");
            DropIndex("dbo.Students", new[] { "ApplyStudent_AppliedID" });
            DropIndex("dbo.Positions", new[] { "ApplyPosition_AppliedID" });
            DropPrimaryKey("dbo.Applieds");
            AlterColumn("dbo.Applieds", "AppliedID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Applieds", "AppliedID");
            RenameColumn(table: "dbo.Students", name: "ApplyStudent_AppliedID", newName: "AppliedID");
            RenameColumn(table: "dbo.Positions", name: "ApplyPosition_AppliedID", newName: "AppliedID");
            CreateIndex("dbo.Applieds", "AppliedID");
        }
    }
}
