using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Utils.Extensions
{
    public static class DataTableExtensions
    {
        public static string ConvertDataTableToHTML(this DataTable dt)
        {
            string html = "<table class=\"table\">";

            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() ?? "-" + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
    }
}
