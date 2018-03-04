namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexTypeLinkInLinkBetweenRequirements : DbMigration
    {
        public override void Up()
        {
            DropIndex("LINK.LINK_BETWEEN_REQUIREMENT", new[] { "TypeLinkID" });
            DropIndex("LINK.LINK_BETWEEN_REQUIREMENT", "IX_LINK_BETWEEN_REQUIREMENT");
            CreateIndex("LINK.LINK_BETWEEN_REQUIREMENT", new[] { "RequirementOriginID", "RequirementTargetID", "TypeLinkID" }, unique: true, name: "IX_LINK_BETWEEN_REQUIREMENT");
        }
        
        public override void Down()
        {
            DropIndex("LINK.LINK_BETWEEN_REQUIREMENT", "IX_LINK_BETWEEN_REQUIREMENT");
            CreateIndex("LINK.LINK_BETWEEN_REQUIREMENT", new[] { "RequirementOriginID", "RequirementTargetID" }, unique: true, name: "IX_LINK_BETWEEN_REQUIREMENT");
            CreateIndex("LINK.LINK_BETWEEN_REQUIREMENT", "TypeLinkID");
        }
    }
}