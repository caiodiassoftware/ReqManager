using System.Web.Mvc;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System.Net;
using System;
using AutoMapper;
using ReqManager.Services.Extensions;

namespace ReqManager.Controllers
{
    public class RequirementRationaleController : BaseController<RequirementRationaleEntity>
    {
        private IRequirementService requirement { get; set; }

        public RequirementRationaleController(
            IRequirementRationaleService service,
            IRequirementTypeService typeService,
            IUserService userService,
            IMeasureImportanceService measureService,
            IRequirementStatusService statusService,
            IRequirementService reqService,
            IStakeholdersProjectService stakeholderProjectService,
            IRequirementService requirement) : base(service)
        {
            this.requirement = requirement;

            ViewData.Add("RequirementID", new SelectList(reqService.getAll(), "RequirementID", "code"));
            ViewData.Add("StakeholdersProjectID", new SelectList(stakeholderProjectService.getAll(), "StakeholdersProjectID", "DisplayName"));
            ViewData.Add("MeasureImportanceID", new SelectList(measureService.getAll(), "MeasureImportanceID", "description"));
            ViewData.Add("RequirementStatusID", new SelectList(statusService.getAll(), "RequirementStatusID", "description"));
            ViewData.Add("RequirementTypeID", new SelectList(typeService.getAll(), "RequirementTypeID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        public ActionResult Rationale(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Mapper.Initialize(cfg =>
                {
                    cfg.CreateAutomaticMapping<RequirementEntity, RequirementRationaleEntity>();
                });

                RequirementEntity entity = requirement.get(id);
                RequirementRationaleEntity rationale = Mapper.Map<RequirementEntity, RequirementRationaleEntity>(entity);

                return View(rationale);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
