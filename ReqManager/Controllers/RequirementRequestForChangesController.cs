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

        public RequirementRequestForChangesController(
            IRequirementRequestForChangesService service,
            IStakeholderRequirementService stakeholderRequirement,
            IStakeholderRequirementApprovalService stakeholders,
            IRequestStatusService status,
            IRequirementService requirement) : base(service)
        {
            this.stakeholderRequirement = stakeholderRequirement;
            this.requirement = requirement;
            this.stakeholders = stakeholders;
            this.service = service;
            ViewData.Add("RequestStatusID", new SelectList(status.getAll(), "RequestStatusID", "description"));
        }

        public ActionResult RequestNewChange(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                RequirementEntity req = requirement.get(id);
                StakeholderRequirementEntity stakeholderRaequirement = stakeholderRequirement.get(getIdUser(), Convert.ToInt32(id));

                if (req == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (stakeholderRaequirement == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (service.validateRequestForRequirement(Convert.ToInt32(id)))
                {
                    ViewData.Add("RequirementID", new SelectList(
                        new List<RequirementEntity>() { req }, "RequirementID", "DisplayName", id));
                    return View();
                }
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
                RequirementEntity req = requirement.get(id);

                RequirementRequestForChangesEntity reqRequest = new RequirementRequestForChangesEntity();
                reqRequest.StakeholderRequirementID = stakeholderRequirement.get(getIdUser(), id).StakeholderRequirementID;
                reqRequest.RequestStatusID = 1;
                reqRequest.creationDate = DateTime.Now;
                reqRequest.request = request;
                base.Create(reqRequest);
                Response.Redirect("~/Requirement/Details/" + id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
