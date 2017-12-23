namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCostToRequirementAndProjectAndStakeholderImportance : DbMigration
    {
        public override void Up()
        {
            AddColumn("REQ.REQUIREMENT", "cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("REQ.REQUIREMENT", "active", c => c.Boolean(nullable: false));
            AddColumn("PROJ.PROJECT", "cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("PROJ.STAKEHOLDERS_PROJECT", "importanceValue", c => c.Int(nullable: false));
            AddColumn("PROJ.STAKEHOLDER_REQUIREMENT", "importanceValue", c => c.Int(nullable: false));
            AddColumn("REQ.REQUIREMENT_TYPE", "abbreviation", c => c.String(maxLength: 4));
            Sql("UPDATE REQ.REQUIREMENT_TYPE SET abbreviation = 'US' WHERE RequirementTypeID = 1");
            Sql("UPDATE REQ.REQUIREMENT_TYPE SET abbreviation = 'RF' WHERE RequirementTypeID = 2");
            Sql("UPDATE REQ.REQUIREMENT_TYPE SET abbreviation = 'RNF' WHERE RequirementTypeID = 3");
            AddColumn("REQ.CHARACTERISTICS", "required", c => c.Boolean(nullable: false));
            AlterColumn("REQ.CHARACTERISTICS", "description", c => c.String(nullable: false, maxLength: 500));
            CreateIndex("REQ.REQUIREMENT_TYPE", "abbreviation", unique: true);
            DropColumn("REQ.CHARACTERISTICS", "active");
        }
        
        public override void Down()
        {
            AddColumn("REQ.CHARACTERISTICS", "active", c => c.Boolean(nullable: false));
            DropIndex("REQ.REQUIREMENT_TYPE", new[] { "abbreviation" });
            AlterColumn("REQ.CHARACTERISTICS", "description", c => c.String(nullable: false, maxLength: 255));
            DropColumn("REQ.CHARACTERISTICS", "required");
            DropColumn("REQ.REQUIREMENT_TYPE", "abbreviation");
            DropColumn("PROJ.STAKEHOLDER_REQUIREMENT", "importanceValue");
            DropColumn("PROJ.STAKEHOLDERS_PROJECT", "importanceValue");
            DropColumn("PROJ.PROJECT", "cost");
            DropColumn("REQ.REQUIREMENT", "active");
            DropColumn("REQ.REQUIREMENT", "cost");
        }
    }
}
