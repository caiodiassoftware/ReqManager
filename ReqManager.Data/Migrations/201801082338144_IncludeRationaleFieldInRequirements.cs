namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncludeRationaleFieldInRequirements : DbMigration
    {
        public override void Up()
        {
            AddColumn("REQ.REQUIREMENT", "rationale", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("REQ.REQUIREMENT", "rationale");
        }
    }
}
