namespace ReqManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.SqlClient;
    using System.IO;

    internal sealed class Configuration : DbMigrationsConfiguration<ReqManager.Data.DataAcess.ReqManagerEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ReqManager.Data.DataAcess.ReqManagerEntities";
        }

        protected override void Seed(ReqManager.Data.DataAcess.ReqManagerEntities context)
        {

        }
    }
}
