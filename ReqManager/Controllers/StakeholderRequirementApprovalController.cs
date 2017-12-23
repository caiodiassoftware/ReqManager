using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Net;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Entities.Requirement;

namespace ReqManager.Controllers
{
    public class StakeholderRequirementApprovalController : BaseController<StakeholderRequirementApprovalEntity>
    {
        private IStakeholderRequirementApprovalService service { get; set; }
        private IStakeholdersService stakeholder { get; set; }
        private IRequirementService requirement { get; set; }
        private IStakeholderRequirementService stakeholderRequirementService { get; set; }
        private IStakeholdersProjectService stakeholderProjectService { get; set; }

        public StakeholderRequirementApprovalController(
            IStakeholderRequirementApprovalService service,
            IStakeholdersService stakeholder,
            IRequirementService requirement,
            IStakeholderRequirementService stakeholderRequirementService,
            IStakeholdersProjectService stakeholderProjectService,
            IProjectService projectService) : base(service)
        {
            this.stakeholderProjectService = stakeholderProjectService;
            this.stakeholderRequirementService = stakeholderRequirementService;
            this.requirement = requirement;
            this.service = service;
            this.stakeholder = stakeholder;

            ViewBag.ProjectID = new SelectList(projectService.getAll(), "ProjectID", "DisplayName");

        }

        public ActionResult Approve(int? id)
        {
            try
            {
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                StakeholderRequirementEntity stakeholder = stakeholderRequirementService.get(Convert.ToInt32(id), getIdUser());

                if (stakeholder == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "You are not Stakeholder interesed in this Requirement!");
                }

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public void Approve(int ID, string description, bool? approved)
        {
            try
            {
                StakeholderRequirementEntity stakeholderRequirement = stakeholderRequirementService.get(ID, getIdUser());

                StakeholderRequirementApprovalEntity approval = new StakeholderRequirementApprovalEntity();
                approval.StakeholderRequirementID = stakeholderRequirement.StakeholderRequirementID;
                approval.description = description;
                approval.approved = Convert.ToBoolean(approved);

                if (TryValidateModel(approval))
                {
                    Service.add(ref approval);
                    ModelState.Clear();
                    TempData["ControllerMessage"] = String.Format("Register was made with Success!");
                    Response.Redirect("~/Requirement/Details/" + ID);
                }
                else
                {
                    getModelStateValidations();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
