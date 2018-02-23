using ReqManager.Entities.Requirement;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.ManagerController;
using System.Web.Mvc;
using System;
using System.Linq;

namespace ReqManager.Controllers
{
    public class RequirementSubTypeController : BaseController<RequirementSubTypeEntity>
    {
        private IRequirementSubTypeService service { get; set; }

        public RequirementSubTypeController(
            IRequirementSubTypeService service,
            IRequirementTypeService type) : base(service)
        {
            this.service = service;
            ViewData.Add("RequirementTypeID", new SelectList(type.getAll(), "RequirementTypeID", "description"));
        }

        public JsonResult GetSubType(int type)
        {
            try
            {
                JsonResult json = Json(service.filterByRequirementType(type), 
                    JsonRequestBehavior.AllowGet);
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
