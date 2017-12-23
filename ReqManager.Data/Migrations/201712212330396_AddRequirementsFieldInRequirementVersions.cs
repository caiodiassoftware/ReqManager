namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequirementsFieldInRequirementVersions : DbMigration
    {
        public override void Up()
        {
            AddColumn("REQ.REQUIREMENT_VERSIONS", "preTraceability", c => c.Boolean(nullable: false));
            AddColumn("REQ.REQUIREMENT_VERSIONS", "cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("REQ.REQUIREMENT_VERSIONS", "active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("REQ.REQUIREMENT_VERSIONS", "active");
            DropColumn("REQ.REQUIREMENT_VERSIONS", "cost");
            DropColumn("REQ.REQUIREMENT_VERSIONS", "preTraceability");
        }
    }
}
