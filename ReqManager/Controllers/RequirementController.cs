using System.Web.Mvc;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System;
using ReqManager.Services.Link.Interfaces;
using AutoMapper;
using ReqManager.Services.Extensions;
using ReqManager.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ReqManager.Controllers
{
    //REQ1
    //R-R1
    //R-A1
    //PRJ4
    public class RequirementController : BaseController<RequirementEntity>
    {
        private IRequirementRationaleService rationaleService { get; set; }
        private IRequirementActionHistoryService reqActionHistoryService {get;set;}
        private ILinkBetweenRequirementsService linkRequirementService { get; set; }
        private ILinkBetweenRequirementsArtifactsService linkReqArtifactService { get; set; }

        public RequirementController(
            IRequirementService service,
            IMeasureImportanceService measureService,
            IRequirementStatusService statusService,
            IRequirementTemplateService templateService,
            IRequirementTypeService typeService,
            IUserService userService,
            IStakeholdersProjectService stakeholderProjectService,
            IRequirementRationaleService rationaleService,
            IRequirementActionHistoryService reqActionHistoryService,
            ILinkBetweenRequirementsService linkRequirementService,
            ILinkBetweenRequirementsArtifactsService linkReqArtifactService) : base(service)
        {
            this.linkRequirementService = linkRequirementService;
            this.linkReqArtifactService = linkReqArtifactService;
            this.rationaleService = rationaleService;
            this.reqActionHistoryService = reqActionHistoryService;

            ViewData.Add("StakeholdersProjectID", new SelectList(stakeholderProjectService.getAll(), "StakeholdersProjectID", "DisplayName"));
            ViewData.Add("MeasureImportanceID",new SelectList(measureService.getAll(), "MeasureImportanceID", "description"));
            ViewData.Add("RequirementStatusID" , new SelectList(statusService.getAll(), "RequirementStatusID", "description"));
            ViewData.Add("RequirementTemplateID", new SelectList(templateService.getAll(), "RequirementTemplateID", "description"));
            ViewData.Add("RequirementTypeID" ,new SelectList(typeService.getAll(), "RequirementTypeID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        public override ActionResult Details(int? id)
        {
            try
            {
                RequirementViewModel req = new RequirementViewModel
                {
                    requirement = Service.get(id),
                    linkReq = linkRequirementService.getAll().Where(r => r.RequirementOriginID.Equals(id) || r.RequirementTargetID.Equals(id)).ToList(),
                    linkReqArt = linkReqArtifactService.getAll().Where(r => r.RequirementID.Equals(id)).ToList()
                };
                return View(req);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(RequirementEntity entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.update(entity);

                    RequirementActionHistoryEntity reqAction = new RequirementActionHistoryEntity();
                    RequirementEntity req = Service.get(entity.RequirementID);
                    reqAction.RequirementID = req.RequirementID;
                    reqAction.DescriptionStatus = req.RequirementStatus.description;
                    reqAction.UserLogin = getLoginUser();

                    reqActionHistoryService.add(reqAction);

                    Service.saveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    getModelStateValidations();
                }

                return View(entity);
            }
            catch (DbEntityValidationException ex)
            {
                return getMessageDbValidation(entity, ex);
            }
            catch (DbUpdateException ex)
            {
                return getMessageDbUpdateException(entity, ex);
            }
            catch (Exception ex)
            {
                return getMessageGeralException(entity, ex);
            }
        }
    }
}
