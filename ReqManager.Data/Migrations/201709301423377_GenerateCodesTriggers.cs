namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    public partial class GenerateCodesTriggers : DbMigration
    {
        public override void Up()
        {
            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../ScriptSql/GenerateCodesScript.sql");
            Sql(File.ReadAllText(sqlFile));
        }
        
        public override void Down()
        {
        }
    }
}
