﻿using ReqManager.Entities.Project;
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
        private IStakeholderRequirementService stakeholders { get; set; }

        public RequirementRequestForChangesController(
            IRequirementRequestForChangesService service,
            IStakeholderRequirementService stakeholders,
            IRequestStatusService status,
            IRequirementService requirement) : base(service)
        {
            this.requirement = requirement;
            this.stakeholders = stakeholders;
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

                ViewData.Add("RequirementID", new SelectList(requirement.getAll(), "RequirementID", "DisplayName", id));
                ViewData.Add("StakeHolderRequirementID", new SelectList(
                    new List<StakeholderRequirementEntity>() { stakeholders.filterByUser(getIdUser()) }, "StakeHolderRequirementID", "DisplayName"));
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public void RequestNewChange(string request, DateTime creationDate, int id)
        {
            try
            {
                RequirementRequestForChangesEntity req = new RequirementRequestForChangesEntity();
                req.RequirementID = Convert.ToInt32(id);
                req.StakeHolderRequirementID = stakeholders.filterByUser(getIdUser()).StakeHolderRequirementID;
                req.RequestStatusID = 1;
                req.creationDate = DateTime.Now;
                req.request = request;
                base.Create(req);
                Response.Redirect("~/Requirement/Details/" + id);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}