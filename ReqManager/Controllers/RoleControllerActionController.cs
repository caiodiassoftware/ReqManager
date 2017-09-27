
using ReqManager.Entities.Acess;
using ReqManager.ManagerController;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.InterfacesServices;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class RoleControllerActionController : BaseController<RoleControllerActionEntity>
    {
        private IRoleControllerActionService service { get; set; }
        private IControllerActionService caService { get; set; }
        private IRoleService roleService { get; set; }

        public RoleControllerActionController(IRoleControllerActionService service, IControllerActionService caService, IRoleService roleService) : base(service)
        {
            this.service = service;
            this.caService = caService;
            this.roleService = roleService;
        }

        #region GETS

        public override ActionResult Create()
        {
            return dropDowns();
        }

        public override ActionResult Edit(int? id)
        {
            base.Edit(id);
            return dropDowns(Service.get(id));
        }

        public override ActionResult Delete(int? id)
        {
            base.Delete(id);
            return dropDowns(Service.get(id));
        }

        #endregion

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(RoleControllerActionEntity RoleControllerActionEntity)
        {
            base.Create(RoleControllerActionEntity);
            return dropDowns();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(RoleControllerActionEntity RoleControllerActionEntity)
        {
            base.Edit(RoleControllerActionEntity);
            return dropDowns();
        }

        #endregion

        #region Private Methods

        private ActionResult dropDowns(RoleControllerActionEntity entity = null)
        {
            ViewBag.ControllerActionID = new SelectList(caService.getAll(), "ControllerActionID", "DisplayName");
            ViewBag.RoleID = new SelectList(roleService.getAll(), "RoleID", "description");
            return entity == null ? View() : View(entity);
        }

        #endregion

    }
}
