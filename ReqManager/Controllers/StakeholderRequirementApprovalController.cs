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
        private IStakeholdersProjectService stakeholderProjectService { get; set; }
        private IRequirementService requirement { get; set; }

        public StakeholderRequirementApprovalController(
            IStakeholderRequirementApprovalService service,
            IStakeholdersService stakeholder,
            IRequirementService requirement,
            IStakeholdersProjectService stakeholderProjectService,
            IProjectService projectService) : base(service)
        {
            this.requirement = requirement;
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
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                RequirementEntity req = requirement.get(id);
                if (req == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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
                RequirementEntity req = requirement.get(ID);
                StakeholdersProjectEntity stake = stakeholderProjectService.getByRequirementAndUser(req.ProjectID, getIdUser());

                StakeholderRequirementApprovalEntity approval = new StakeholderRequirementApprovalEntity();
                approval.RequirementID = req.RequirementID;
                approval.StakeholdersProjectID = stake.StakeholdersProjectID;
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
