namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequirementVersions : DbMigration
    {
        public override void Up()
        {
            DropIndex("REQ.REQUIREMENT_VERSIONS", "IX_REQUIREMENT_VERSIONS");
            DropIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementTemplateID" });
            AddColumn("REQ.REQUIREMENT_VERSIONS", "RequirementID", c => c.Int());
            AlterColumn("REQ.REQUIREMENT_VERSIONS", "RequirementRequestForChangesID", c => c.Int());
            AlterColumn("REQ.REQUIREMENT_VERSIONS", "RequirementTemplateID", c => c.Int());
            CreateIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementRequestForChangesID", "versionNumber" }, unique: true, name: "IX_REQUIREMENT_VERSIONS");
            CreateIndex("REQ.REQUIREMENT_VERSIONS", "RequirementID");
            CreateIndex("REQ.REQUIREMENT_VERSIONS", "RequirementTemplateID");
            AddForeignKey("REQ.REQUIREMENT_VERSIONS", "RequirementID", "REQ.REQUIREMENT", "RequirementID");
        }
        
        public override void Down()
        {
            DropForeignKey("REQ.REQUIREMENT_VERSIONS", "RequirementID", "REQ.REQUIREMENT");
            DropIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementTemplateID" });
            DropIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementID" });
            DropIndex("REQ.REQUIREMENT_VERSIONS", "IX_REQUIREMENT_VERSIONS");
            AlterColumn("REQ.REQUIREMENT_VERSIONS", "RequirementTemplateID", c => c.Int(nullable: false));
            AlterColumn("REQ.REQUIREMENT_VERSIONS", "RequirementRequestForChangesID", c => c.Int(nullable: false));
            DropColumn("REQ.REQUIREMENT_VERSIONS", "RequirementID");
            CreateIndex("REQ.REQUIREMENT_VERSIONS", "RequirementTemplateID");
            CreateIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementRequestForChangesID", "versionNumber" }, unique: true, name: "IX_REQUIREMENT_VERSIONS");
        }
    }
}
