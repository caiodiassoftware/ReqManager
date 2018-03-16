using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Data;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class DataSetController : Controller
    {
        #region Attributes

        private IRequirementService requirementService { get; set; }

        #endregion

        #region Constructor

        public DataSetController(IRequirementService requirementService)
        {
            this.requirementService = requirementService;
        }

        #endregion

        #region Xls

        public void DataSetDependencies(int ProjectID = 0)
        {
            string name = System.Reflection.MethodBase.GetCurrentMethod().Name + DateTime.Now.ToString();
            renderDataSetXls(requirementService.DataSetDependencies(ProjectID), name);
        }

        public void DataSetPriorities(int ProjectID = 0)
        {
            string name = System.Reflection.MethodBase.GetCurrentMethod().Name + DateTime.Now.ToString();
            renderDataSetXls(requirementService.DataSetPriorities(ProjectID), name);
        }

        public void DataSetRequirementsCost(int ProjectID = 0)
        {
            string name = System.Reflection.MethodBase.GetCurrentMethod().Name + DateTime.Now.ToString();
            renderDataSetXls(requirementService.DataSetRequirementsCost(ProjectID), name);
        }

        public void DataSetStakeholderImportances(int ProjectID = 0)
        {
            string name = System.Reflection.MethodBase.GetCurrentMethod().Name + DateTime.Now.ToString();
            renderDataSetXls(requirementService.DataSetStakeholderImportances(ProjectID), name);
        }

        #endregion

        #region Private Methods

        private void renderDataSetXls(DataTable dt, string name)
        {
            try
            {
                string attachment = "attachment; filename=" + name + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void renderDataSetJson(DataTable dt, string name)
        {
            try
            {
                string json = JsonConvert.SerializeObject(dt);
                string jsonFormatted = JToken.Parse(json).ToString(Formatting.Indented);

                string attachment = "attachment; filename=" + name + ".txt";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "text/plain";
                Response.Write(jsonFormatted);
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}