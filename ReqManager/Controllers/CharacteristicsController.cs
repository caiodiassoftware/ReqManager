using DataTables.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
                IEnumerable<CharacteristicsEntity> filtered = Service.getAll();

                if (!string.IsNullOrEmpty(requestModel.Search.Value))
                {
                    filtered = filtered.Where(c => c.name.Contains(requestModel.Search.Value)
                               ||
                    c.description.Contains(requestModel.Search.Value)
                               ||
                               c.active.ToString().Contains(requestModel.Search.Value));
                }

                var result = from c in filtered
                             select new[] { c.name, c.description, c.active.ToString() };

                return Json(new
                {
                    sEcho = requestModel.Search.Value,
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
