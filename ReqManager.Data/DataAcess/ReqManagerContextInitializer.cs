using System;
using System.Data.Entity;
using System.IO;

namespace ReqManager.Data.DataAcess
{
    public class ReqManagerContextInitializer : CreateDatabaseIfNotExists<ReqManagerEntities>
    {
        protected override void Seed(ReqManagerEntities context)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativeDirectory = baseDirectory.Remove(baseDirectory.Length - 12);

            var sqlFile = Path.Combine(
                relativeDirectory, @"ReqManager.Utils\Script\GenerateCodesScript.sql");
            string ScriptGenerateCodesTriggers = System.IO.File.ReadAllText(sqlFile);

            context.Database.ExecuteSqlCommand(ScriptGenerateCodesTriggers);
            base.Seed(context);
        }
    }
}
