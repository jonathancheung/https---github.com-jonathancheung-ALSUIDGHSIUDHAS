namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcreaterecruiter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RecruiterCompanies",
                c => new
                    {
                        Recruiter_RecruiterID = c.Int(nullable: false),
                        Company_CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recruiter_RecruiterID, t.Company_CompanyID })
                .ForeignKey("dbo.Recruiters", t => t.Recruiter_RecruiterID, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.Company_CompanyID, cascadeDelete: true)
                .Index(t => t.Recruiter_RecruiterID)
                .Index(t => t.Company_CompanyID);
            
            AddColumn("dbo.AspNetUsers", "Recruiters_RecruiterID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Recruiters_RecruiterID");
            AddForeignKey("dbo.AspNetUsers", "Recruiters_RecruiterID", "dbo.Recruiters", "RecruiterID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Recruiters_RecruiterID", "dbo.Recruiters");
            DropForeignKey("dbo.RecruiterCompanies", "Company_CompanyID", "dbo.Companies");
            DropForeignKey("dbo.RecruiterCompanies", "Recruiter_RecruiterID", "dbo.Recruiters");
            DropIndex("dbo.RecruiterCompanies", new[] { "Company_CompanyID" });
            DropIndex("dbo.RecruiterCompanies", new[] { "Recruiter_RecruiterID" });
            DropIndex("dbo.AspNetUsers", new[] { "Recruiters_RecruiterID" });
            DropColumn("dbo.AspNetUsers", "Recruiters_RecruiterID");
            DropTable("dbo.RecruiterCompanies");
        }
    }
}
