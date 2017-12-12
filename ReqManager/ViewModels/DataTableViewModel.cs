using System.Data;

namespace ReqManager.ViewModels
{
    public class DataTableViewModel
    {
        public DataTable dataTable { get; set; }

        public DataTableViewModel()
        {
            dataTable = new DataTable();
        }
    }
}