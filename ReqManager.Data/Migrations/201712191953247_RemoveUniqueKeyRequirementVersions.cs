namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUniqueKeyRequirementVersions : DbMigration
    {
        public override void Up()
        {
            DropIndex("REQ.REQUIREMENT_VERSIONS", "IX_REQUIREMENT_VERSIONS");
            CreateIndex("REQ.REQUIREMENT_VERSIONS", "RequirementRequestForChangesID");
        }
        
        public override void Down()
        {
            DropIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementRequestForChangesID" });
            CreateIndex("REQ.REQUIREMENT_VERSIONS", new[] { "RequirementRequestForChangesID", "versionNumber" }, unique: true, name: "IX_REQUIREMENT_VERSIONS");
        }
    }
}
