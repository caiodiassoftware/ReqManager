using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class LinkBetweenRequirementsController : BaseController<LinkBetweenRequirementsEntity>
    {
        private ILinkBetweenRequirementsService linkService { get; set; }
        private IRequirementService requirementService { get; set; }
        private ITypeLinkService typeLinkService { get; set; }
        private IUserService userService { get; set; }

        public LinkBetweenRequirementsController(
            ILinkBetweenRequirementsService linkService,
            IRequirementService requirementService,
            ITypeLinkService typeLinkService,
            IUserService userService) : base(linkService)
        {
            this.linkService = linkService;
            this.requirementService = requirementService;
            this.typeLinkService = typeLinkService;
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

        private ActionResult dropDowns(LinkBetweenRequirementsEntity entity = null)
        {
            ViewBag.RequirementOriginID = new SelectList(requirementService.getAll(), "RequirementID", "code");
            ViewBag.RequirementTargetID = new SelectList(requirementService.getAll(), "RequirementID", "code");
            ViewBag.TypeLinkID = new SelectList(typeLinkService.getAll(), "TypeLinkID", "description");
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            return entity == null ? View() : View(entity);
        }

        #endregion
    }
}
