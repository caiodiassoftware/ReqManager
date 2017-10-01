using System.Web.Mvc;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System;

namespace ReqManager.Controllers
{
    public class RequirementController : BaseController<RequirementEntity>
    {
        private IRequirementRationaleService rationaleService { get; set; }
        private IRequirementActionHistoryService reqActionHistoryService {get;set;}

        public RequirementController(
            IRequirementService service,
            IMeasureImportanceService measureService,
            IRequirementStatusService statusService,
            IRequirementTemplateService templateService,
            IRequirementTypeService typeService,
            IUserService userService,
            IStakeholdersProjectService stakeholderProjectService,
            IRequirementRationaleService rationaleService,
            IRequirementActionHistoryService reqActionHistoryService) : base(service)
        {
            this.rationaleService = rationaleService;
            this.reqActionHistoryService = reqActionHistoryService;

            ViewData.Add("StakeholdersProjectID", new SelectList(stakeholderProjectService.getAll(), "StakeholdersProjectID", "DisplayName"));
            ViewData.Add("MeasureImportanceID",new SelectList(measureService.getAll(), "MeasureImportanceID", "description"));
            ViewData.Add("RequirementStatusID" , new SelectList(statusService.getAll(), "RequirementStatusID", "description"));
            ViewData.Add("RequirementTemplateID", new SelectList(templateService.getAll(), "RequirementTemplateID", "description"));
            ViewData.Add("RequirementTypeID" ,new SelectList(typeService.getAll(), "RequirementTypeID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
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
