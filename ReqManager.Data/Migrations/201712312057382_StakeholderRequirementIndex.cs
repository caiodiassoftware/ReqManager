namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StakeholderRequirementIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("PROJ.STAKEHOLDER_REQUIREMENT", new[] { "StakeholdersProjectID" });
            DropIndex("PROJ.STAKEHOLDER_REQUIREMENT", new[] { "RequirementID" });
            CreateIndex("PROJ.STAKEHOLDER_REQUIREMENT", new[] { "StakeholdersProjectID", "RequirementID" }, unique: true, name: "IX_STAKEHOLDER_REQUIREMENT");
        }
        
        public override void Down()
        {
            DropIndex("PROJ.STAKEHOLDER_REQUIREMENT", "IX_STAKEHOLDER_REQUIREMENT");
            CreateIndex("PROJ.STAKEHOLDER_REQUIREMENT", "RequirementID");
            CreateIndex("PROJ.STAKEHOLDER_REQUIREMENT", "StakeholdersProjectID");
        }
    }
}
