using DataTables.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class CharacteristicsController : BaseController<CharacteristicsEntity>
    {
        public CharacteristicsController(IService<CharacteristicsEntity> service) : base(service)
        {
        }

        public ActionResult GetFilter([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            try
            {
                var characteristics = Service.getAll();
                List<CharacteristicsEntity> filtered = new List<CharacteristicsEntity>();
                IEnumerable<string[]> result;
                string searchValue = requestModel.Search.Value;

                if (!string.IsNullOrEmpty(searchValue))
                {
                    foreach (CharacteristicsEntity item in characteristics)
                    {
                        foreach (PropertyInfo pi in item.GetType().GetProperties())
                        {
                            string value = pi.GetValue(item).ToString();
                            if (value.Contains(searchValue))
                            {
                                filtered.Add(item);
                                break;
                            }
                        }
                    }

                    result = from c in filtered
                             select new[] { c.name, c.description, c.active.ToString() };
                }
                else
                {
                    result = from c in characteristics.Take(5)
                             select new[] { c.name, c.description, c.active.ToString() };
                }

                return Json(new
                {
                    //sEcho = requestModel.Search.Value,
                    iTotalRecords = 97,
                    iTotalDisplayRecords = 3,
                    aaData = result
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
