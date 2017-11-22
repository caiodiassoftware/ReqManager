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

        public ActionResult GetFilter()
        {
            try
            {
                IEnumerable<CharacteristicsEntity> filtered = Service.getAll();

                string filterName = Request.QueryString["name"];

                if (!string.IsNullOrEmpty(filterName))
                {
                    filtered = filtered.Where(c => c.name.Contains(filterName)
                               ||
                    c.description.Contains(filterName)
                               ||
                               c.active.ToString().Contains(filterName));
                }

                var result = from c in filtered
                             select new[] { c.name, c.description, c.active.ToString() };

                return Json(new
                {
                    sEcho = filterName,
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
