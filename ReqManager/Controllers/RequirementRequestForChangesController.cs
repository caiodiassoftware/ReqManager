using ReqManager.Entities.Project;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    public class RequirementRequestForChangesController : BaseController<RequirementRequestForChangesEntity>
    {
        private IRequirementService requirement { get; set; }
        private IStakeholderRequirementApprovalService stakeholders { get; set; }
        private IRequirementRequestForChangesService service { get; set; }
        private IStakeholderRequirementService stakeholderRequirement { get; set; }
        private IStakeholdersProjectService stakeholderProject { get; set; }

        public RequirementRequestForChangesController(
            IRequirementRequestForChangesService service,
            IStakeholderRequirementService stakeholderRequirement,
            IStakeholderRequirementApprovalService stakeholders,
            IRequestStatusService status,
            IStakeholdersProjectService stakeholderProject,
            IRequirementService requirement) : base(service)
        {
            this.stakeholderProject = stakeholderProject;
            this.stakeholderRequirement = stakeholderRequirement;
            this.requirement = requirement;
            this.stakeholders = stakeholders;
            this.service = service;
            ViewData.Add("RequestStatusID", new SelectList(status.getAll(), "RequestStatusID", "description"));
        }

        [HttpPost]
        public void ChangeStatus(int RequirementRequestForChangesID, int RequestStatusID)
        {
            try
            {
                RequirementRequestForChangesEntity request = Service.get(RequirementRequestForChangesID);
                request.RequestStatusID = RequestStatusID;
                Service.update(ref request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HandleError(ExceptionType = typeof(Exception), View = "Error")]
        public ActionResult RequestNewChange(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentException("Invalid Request!");
                }

                StakeholderRequirementEntity stakeholder = stakeholderRequirement.get(Convert.ToInt32(id), getIdUser());

                if (stakeholder == null)
                {
                    info("You are not a Stakeholder bound by this Requirement!");
                    return RedirectToAction("Details", "Requirement", new { id = id });
                }

                if (service.validateRequestForRequirement(Convert.ToInt32(id)))
                {
                    ViewData.Add("RequirementID", new SelectList(
                        new List<RequirementEntity>() { stakeholder.Requirement }, "RequirementID", "DisplayName", id));
                    return View();
                }
                else
                {
                    info("This requirement already has a change request!");
                    return RedirectToAction("Details", "Requirement", new { id = id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public void RequestNewChange(string request, int id)
        {
            try
            {
                StakeholderRequirementEntity stakeholder = stakeholderRequirement.get(id, getIdUser());

                RequirementRequestForChangesEntity reqRequest = new RequirementRequestForChangesEntity();
                reqRequest.StakeholderRequirementID = stakeholder.StakeholderRequirementID;
                reqRequest.RequestStatusID = 1;
                reqRequest.creationDate = DateTime.Now;
                reqRequest.request = request;
                Service.add(ref reqRequest);
                Response.Redirect("~/Requirement/Details/" + id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
