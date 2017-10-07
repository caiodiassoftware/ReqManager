using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ReqManager.ViewModels
{
    public class DataTableViewModel
    {
        public DataTableViewModel()
        {
            dataTable = new DataTable();
        }

        public DataTable dataTable { get; set; }
    }
}