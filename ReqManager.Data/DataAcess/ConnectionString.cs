namespace ReqManager.Data.DataAcess
{
    public class ConnectionString
    {
        private static string DataSource = "FAMILIA-DIAS";
        private static string InitialCatalog = "TAADSPRequirements";
        private static string User = "sa";
        private static string Password = "CaioDias18";

        public static string GetConnectionString()
        {
            return @"Data Source="+ DataSource +
                    ";Initial Catalog="+ InitialCatalog +
                    ";Persist Security Info=True;User ID="+ User +
                    ";Password=" + Password;
        }
    }
}
