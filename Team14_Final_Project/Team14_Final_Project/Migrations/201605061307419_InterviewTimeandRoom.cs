namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterviewTimeandRoom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InterviewRooms",
                c => new
                    {
                        InterviewRoomID = c.Int(nullable: false, identity: true),
                        Rooms = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InterviewRoomID);
            
            CreateTable(
                "dbo.InterviewTimes",
                c => new
                    {
                        InterviewTimesID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        StartTimeHour = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InterviewTimesID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InterviewTimes");
            DropTable("dbo.InterviewRooms");
        }
    }
}
