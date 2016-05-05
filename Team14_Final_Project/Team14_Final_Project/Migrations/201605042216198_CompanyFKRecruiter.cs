namespace Team14_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyFKRecruiter : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecruiterCompanies", "Recruiter_RecruiterID", "dbo.Recruiters");
            DropForeignKey("dbo.RecruiterCompanies", "Company_CompanyID", "dbo.Companies");
            DropIndex("dbo.RecruiterCompanies", new[] { "Recruiter_RecruiterID" });
            DropIndex("dbo.RecruiterCompanies", new[] { "Company_CompanyID" });
            AddColumn("dbo.Recruiters", "Company_CompanyID", c => c.Int());
            CreateIndex("dbo.Recruiters", "Company_CompanyID");
            AddForeignKey("dbo.Recruiters", "Company_CompanyID", "dbo.Companies", "CompanyID");
            DropTable("dbo.RecruiterCompanies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecruiterCompanies",
                c => new
                    {
                        Recruiter_RecruiterID = c.Int(nullable: false),
                        Company_CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recruiter_RecruiterID, t.Company_CompanyID });
            
            DropForeignKey("dbo.Recruiters", "Company_CompanyID", "dbo.Companies");
            DropIndex("dbo.Recruiters", new[] { "Company_CompanyID" });
            DropColumn("dbo.Recruiters", "Company_CompanyID");
            CreateIndex("dbo.RecruiterCompanies", "Company_CompanyID");
            CreateIndex("dbo.RecruiterCompanies", "Recruiter_RecruiterID");
            AddForeignKey("dbo.RecruiterCompanies", "Company_CompanyID", "dbo.Companies", "CompanyID", cascadeDelete: true);
            AddForeignKey("dbo.RecruiterCompanies", "Recruiter_RecruiterID", "dbo.Recruiters", "RecruiterID", cascadeDelete: true);
        }
    }
}
