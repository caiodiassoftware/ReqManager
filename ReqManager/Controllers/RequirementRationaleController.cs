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
    public class RequirementRationaleController : BaseController<RequirementRationaleEntity>
    {
        private IRequirementRationaleService service { get; set; }
        private IMeasureImportanceService measureService { get; set; }
        private IRequirementStatusService statusService { get; set; }
        private IStakeholdersProjectService stakeholderProjectService { get; set; }
        private IRequirementTypeService typeService { get; set; }
        private IUserService userService { get; set; }
        private IRequirementService reqService { get; set; }

        public RequirementRationaleController(
            IRequirementRationaleService service,
            IRequirementTypeService typeService,
            IUserService userService,
            IMeasureImportanceService measureService,
            IRequirementStatusService statusService,
            IRequirementService reqService,
            IStakeholdersProjectService stakeholderProjectService) : base(service)
        {
            this.service = service;
            this.typeService = typeService;
            this.userService = userService;
            this.measureService = measureService;
            this.statusService = statusService;
            this.reqService = reqService;
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

        #region Private Methods

        private ActionResult dropDowns(RequirementRationaleEntity entity = null)
        {
            ViewBag.MeasureImportanceID = new SelectList(measureService.getAll(), "MeasureImportanceID", "description");
            ViewBag.RequirementID = new SelectList(reqService.getAll(), "RequirementID", "code");
            ViewBag.RequirementStatusID = new SelectList(statusService.getAll(), "RequirementStatusID", "description");
            ViewBag.RequirementTypeID = new SelectList(typeService.getAll(), "RequirementTypeID", "description");
            ViewBag.StakeholdersProjectID = new SelectList(stakeholderProjectService.getAll(), "StakeholdersProjectID", "StakeholdersProjectID");
            ViewBag.UserID = new SelectList(userService.getAll(), "UserID", "name");
            return entity == null ? View() : View(entity);
        }

        #endregion

    }
}
