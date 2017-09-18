using System.Linq;
using System.Web.Mvc;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.InterfacesServices;

namespace ReqManager.Controllers
{
    public class RoleControllerActionController : BaseController<Entities.Acess.RoleControllerActionEntity>
    {
        private IRoleService roleService { get; set; }
        private IControllerActionService caService { get; set; }

        public RoleControllerActionController(IRoleControllerActionService service, IRoleService roleService, IControllerActionService caService) : base(service)
        {
            this.roleService = roleService;
            this.caService = caService;
        }

        public override ActionResult Create()
        {
            //var caList = caService.getAll().ToList();
            //caList.ForEach(ca => ca.controller = ca.controller + " - " + ca.action);

            //ViewBag.ControllerActionID = new SelectList(caList, "ControllerActionID", "controller");
            //ViewBag.RoleID = new SelectList(roleService.getAll(), "RoleID", "description");
            return View();
        }
    }
}
