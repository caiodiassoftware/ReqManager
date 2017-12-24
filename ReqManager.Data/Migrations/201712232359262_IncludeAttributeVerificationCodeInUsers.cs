namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncludeAttributeVerificationCodeInUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("ACESS.USERS", "verificationCode", c => c.String(maxLength: 6));
        }
        
        public override void Down()
        {
            DropColumn("ACESS.USERS", "verificationCode");
        }
    }
}
