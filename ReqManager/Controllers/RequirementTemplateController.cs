using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class RequirementTemplateController : BaseController<RequirementTemplateEntity>
    {
        private IRequirementTemplateService service { get; set; }
        private IUserService userService { get; set; }

        public RequirementTemplateController(IRequirementTemplateService service, IUserService userService) : base(service)
        {
            this.service = service;
            this.userService = userService;
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

        

        #region Private Methods

        private ActionResult dropDowns(RequirementTemplateEntity entity = null)
        {
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            return entity == null ? View() : View(entity);
        }

        #endregion

    }
}
