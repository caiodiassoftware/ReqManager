using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReqManager.Data.DataAcess;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Controllers
{
    public class StakeholderRequirementController : BaseController<StakeholderRequirementEntity>
    {
        private IStakeholderRequirementService service { get; set; }
        private IStakeholdersService stakeholderService { get; set; }
        private IProjectRequirementsService projectReqService { get; set; }

        public StakeholderRequirementController(
            IStakeholderRequirementService service,
            IStakeholdersService stakeholderService,
            IProjectRequirementsService projectReqService) : base(service)
        {
            this.service = service;
            this.stakeholderService = stakeholderService;
            this.projectReqService = projectReqService;
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
        public override ActionResult Create(StakeholderRequirementEntity StakeholderRequirementEntity)
        {
            base.Create(StakeholderRequirementEntity);
            return dropDowns();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(StakeholderRequirementEntity StakeholderRequirementEntity)
        {
            base.Edit(StakeholderRequirementEntity);
            return dropDowns();
        }

        #endregion

        #region Private Methods

        private ActionResult dropDowns(StakeholderRequirementEntity entity = null)
        {
            ViewBag.ProjectRequirementID = new SelectList(projectReqService.getAll(), "ProjectRequirementID", "DisplayName");
            ViewBag.StakeHolderID = new SelectList(stakeholderService.getAll(), "StakeholderID", "DisplayName");
            return entity == null ? View() : View(entity);
        }

        #endregion

    }
}
