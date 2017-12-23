namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlagCheckInRequirementsCharacteristics : DbMigration
    {
        public override void Up()
        {
            AddColumn("REQ.REQUIREMENT_CHARACTERISTICS", "check", c => c.Boolean(nullable: false));
            DropColumn("REQ.REQUIREMENT_CHARACTERISTICS", "active");
        }
        
        public override void Down()
        {
            AddColumn("REQ.REQUIREMENT_CHARACTERISTICS", "active", c => c.Boolean(nullable: false));
            DropColumn("REQ.REQUIREMENT_CHARACTERISTICS", "check");
        }
    }
}
