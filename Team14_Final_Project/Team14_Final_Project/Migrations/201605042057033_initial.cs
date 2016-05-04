namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        CompanyDescription = c.String(nullable: false),
                        CompanyEmail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            CreateTable(
                "dbo.Industries",
                c => new
                    {
                        IndustryID = c.Int(nullable: false, identity: true),
                        IndustryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IndustryID);
            
            CreateTable(
                "dbo.Majors",
                c => new
                    {
                        MajorID = c.Int(nullable: false, identity: true),
                        MajorName = c.String(),
                    })
                .PrimaryKey(t => t.MajorID);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PositionID = c.Int(nullable: false, identity: true),
                        PositionTitle = c.String(nullable: false),
                        PositionDescription = c.String(),
                        Location = c.String(nullable: false),
                        PositionTypes = c.Int(nullable: false),
                        PositionDeadline = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PositionID);
            
            CreateTable(
                "dbo.Recruiters",
                c => new
                    {
                        RecruiterID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RecruiterID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        EID = c.String(nullable: false),
                        Major = c.Int(nullable: false),
                        GradSemester = c.Int(nullable: false),
                        GradYear = c.Int(nullable: false),
                        StudentPosition = c.Int(nullable: false),
                        GPA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AppUsers_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUsers_Id)
                .Index(t => t.AppUsers_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IndustryCompanies",
                c => new
                    {
                        Industry_IndustryID = c.Int(nullable: false),
                        Company_CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Industry_IndustryID, t.Company_CompanyID })
                .ForeignKey("dbo.Industries", t => t.Industry_IndustryID, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.Company_CompanyID, cascadeDelete: true)
                .Index(t => t.Industry_IndustryID)
                .Index(t => t.Company_CompanyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "AppUsers_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.IndustryCompanies", "Company_CompanyID", "dbo.Companies");
            DropForeignKey("dbo.IndustryCompanies", "Industry_IndustryID", "dbo.Industries");
            DropIndex("dbo.IndustryCompanies", new[] { "Company_CompanyID" });
            DropIndex("dbo.IndustryCompanies", new[] { "Industry_IndustryID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Students", new[] { "AppUsers_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.IndustryCompanies");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Students");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Recruiters");
            DropTable("dbo.Positions");
            DropTable("dbo.Majors");
            DropTable("dbo.Industries");
            DropTable("dbo.Companies");
        }
    }
}
