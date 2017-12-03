using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Net;

namespace ReqManager.Controllers
{
    public class StakeholderRequirementController : BaseController<StakeholderRequirementApprovalEntity>
    {
        private IStakeholderRequirementService service { get; set; }
        private IStakeholdersService stakeholder { get; set; }
        private IStakeholdersProjectService stakeholderProjectService { get; set; }

        public StakeholderRequirementController(
            IStakeholderRequirementService service,
            IStakeholdersService stakeholder,
            IStakeholdersProjectService stakeholderProjectService,
            IProjectService projectService) : base(service)
        {
            this.service = service;
            this.stakeholderProjectService = stakeholderProjectService;
            this.stakeholder = stakeholder;

            ViewBag.ProjectID = new SelectList(projectService.getAll(), "ProjectID", "DisplayName");
            
        }

        public ActionResult Approve(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ViewBag.StakeholdersProjectID = new SelectList(stakeholderProjectService.getAll(), "StakeholdersProjectID", "DisplayName");

                StakeholderRequirementApprovalEntity stakeholderRequirement = null;

                return View(stakeholderRequirement);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Approve(StakeholderRequirementApprovalEntity stakeholderRequirement)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateNewRelationship(int ProjectID, int RequirementID, int StakeHolderID)
        {
            try
            {
                StakeholderRequirementApprovalEntity entity = new StakeholderRequirementApprovalEntity();
                return base.Create(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
