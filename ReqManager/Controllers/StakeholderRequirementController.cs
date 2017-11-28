using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Net;

namespace ReqManager.Controllers
{
    public class StakeholderRequirementController : BaseController<StakeholderRequirementEntity>
    {
        private IProjectRequirementsService projectRequirementService { get; set; }
        private IStakeholderRequirementService service { get; set; }
        private IStakeholdersService stakeholder { get; set; }

        public StakeholderRequirementController(
            IStakeholderRequirementService service,
            IStakeholdersService stakeholder,
            IProjectRequirementsService projectRequirementService,
            IProjectService projectService) : base(service)
        {
            this.service = service;
            this.projectRequirementService = projectRequirementService;
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

                projectRequirementService.getRequirementsByRequirement(Convert.ToInt32(id));

                StakeholderRequirementEntity stakeholderRequirement = service.filterByRequirementAndUser(Convert.ToInt32(id), getIdUser());

                ViewData.Add("StakeHolderID", new SelectList(stakeholder.getAll(), "StakeHolderID", "DisplayName", getIdUser()));

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Approve(StakeholderRequirementEntity stakeholderRequirement)
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
                ProjectRequirementsEntity projectReq = projectRequirementService.getRequirementsByProjectAndRequirement(ProjectID, RequirementID);
                StakeholderRequirementEntity entity = new StakeholderRequirementEntity();
                entity.ProjectRequirementID = projectReq.ProjectRequirementID;
                entity.StakeHolderID = StakeHolderID;
                return base.Create(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
