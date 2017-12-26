using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;

namespace ReqManager.Data.DataAcess
{
    public class ReqManagerContextInitializer : CreateDatabaseIfNotExists<ReqManagerEntities>
    {
        protected override void Seed(ReqManagerEntities context)
        {
        }
    }
}
