namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequirementSubTypeAndTemplateNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("REQ.REQUIREMENT", new[] { "RequirementTemplateID" });
            AlterColumn("REQ.REQUIREMENT", "RequirementTemplateID", c => c.Int());
            CreateIndex("REQ.REQUIREMENT", "RequirementTemplateID");
        }
        
        public override void Down()
        {
            DropIndex("REQ.REQUIREMENT", new[] { "RequirementTemplateID" });
            AlterColumn("REQ.REQUIREMENT", "RequirementTemplateID", c => c.Int(nullable: false));
            CreateIndex("REQ.REQUIREMENT", "RequirementTemplateID");
        }
    }
}
