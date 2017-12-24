namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseVerificationSizeLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ACESS.USERS", "verificationCode", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("ACESS.USERS", "verificationCode", c => c.String(maxLength: 6));
        }
    }
}
