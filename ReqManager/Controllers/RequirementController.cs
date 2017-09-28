using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class RequirementController : BaseController<RequirementEntity>
    {
        private IRequirementService service { get; set; }
        private IMeasureImportanceService measureService { get; set; }
        private IRequirementStatusService statusService { get; set; }
        private IRequirementTemplateService templateService { get; set; }
        private IRequirementTypeService typeService { get; set; }
        private IUserService userService { get; set; }
        private IStakeholdersProjectService stakeholderProjectService { get; set; }

        public RequirementController(
            IRequirementService service,
            IMeasureImportanceService measureService,
            IRequirementStatusService statusService,
            IRequirementTemplateService templateService,
            IRequirementTypeService typeService,
            IUserService userService,
            IStakeholdersProjectService stakeholderProjectService) : base(service)
        {
            this.service = service;
            this.measureService = measureService;
            this.statusService = statusService;
            this.templateService = templateService;
            this.typeService = typeService;
            this.userService = userService;
            this.stakeholderProjectService = stakeholderProjectService;
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
        public override ActionResult Create(RequirementEntity RequirementEntity)
        {
            base.Create(RequirementEntity);
            return dropDowns();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(RequirementEntity RequirementEntity)
        {
            base.Edit(RequirementEntity);
            return dropDowns();
        }

        #endregion

        #region Private Methods

        private ActionResult dropDowns(RequirementEntity entity = null)
        {
            ViewBag.StakeholdersProjectID = new SelectList(stakeholderProjectService.getAll(), "StakeholdersProjectID", "DisplayName");
            ViewBag.MeasureImportanceID = new SelectList(measureService.getAll(), "MeasureImportanceID", "description");
            ViewBag.RequirementStatusID = new SelectList(statusService.getAll(), "RequirementStatusID", "description");
            ViewBag.RequirementTemplateID = new SelectList(templateService.getAll(), "RequirementTemplateID", "description");
            ViewBag.RequirementTypeID = new SelectList(typeService.getAll(), "RequirementTypeID", "description");
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            return entity == null ? View() : View(entity);
        }

        #endregion

    }
}
