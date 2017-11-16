using System.Web.Mvc;
using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System;
using ReqManager.Services.Link.Interfaces;
using AutoMapper;
using ReqManager.Services.Extensions;
using ReqManager.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ReqManager.Controllers
{
    //REQ1
    //R-R1
    //R-A1
    //PRJ4
    public class RequirementController : ControlAcessController<RequirementEntity>
    {
        private IRequirementRationaleService rationaleService { get; set; }
        private IRequirementActionHistoryService reqActionHistoryService { get; set; }
        private ILinkBetweenRequirementsService linkRequirementService { get; set; }
        private ILinkBetweenRequirementsArtifactsService linkReqArtifactService { get; set; }
        private IRequirementService requirementService { get; set; }
        private IProjectService projectService { get; set; }
        private IMeasureImportanceService measureService { get; set; }
        private IRequirementStatusService statusService { get; set; }
        private IRequirementTypeService typeService { get; set; }
        private IUserService userService { get; set; }
        private IStakeholdersProjectService stakeholdersProject { get; set; }
        private IRequirementTemplateService templateService { get; set; }

        public RequirementController(
            IRequirementService requirementService,
            IMeasureImportanceService measureService,
            IRequirementStatusService statusService,
            IRequirementTypeService typeService,
            IUserService userService,
            IRequirementRationaleService rationaleService,
            IRequirementActionHistoryService reqActionHistoryService,
            ILinkBetweenRequirementsService linkRequirementService,
            ILinkBetweenRequirementsArtifactsService linkReqArtifactService,
            IStakeholdersProjectService stakeholdersProject,
            IRequirementTemplateService templateService,
            IProjectService projectService)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateAutomaticMapping<RequirementViewModel, RequirementEntity>();
                cfg.CreateAutomaticMapping<RequirementEntity, RequirementViewModel>();
            });

            this.requirementService = requirementService;
            this.templateService = templateService;
            this.stakeholdersProject = stakeholdersProject;
            this.userService = userService;
            this.measureService = measureService;
            this.projectService = projectService;
            this.statusService = statusService;
            this.typeService = typeService;
            this.linkRequirementService = linkRequirementService;
            this.linkReqArtifactService = linkReqArtifactService;
            this.rationaleService = rationaleService;
            this.reqActionHistoryService = reqActionHistoryService;
        }

        #region GETS

        public ActionResult Index()
        {
            try
            {
                return View(Mapper.Map<IEnumerable<RequirementEntity>, IEnumerable<RequirementViewModel>>(requirementService.getAll()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Details(int? id)
        {
            try
            {
                RequirementViewModel vm = Mapper.Map<RequirementEntity, RequirementViewModel>(requirementService.get(id));
                vm.linkReq = linkRequirementService.getAll().Where(r => r.RequirementOriginID.Equals(id) || r.RequirementTargetID.Equals(id)).ToList();
                vm.linkReqArt = linkReqArtifactService.getAll().Where(r => r.RequirementID.Equals(id)).ToList();

                return View(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Create()
        {
            try
            {
                createViewData();
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RequirementEntity entity = requirementService.get(id);
                RequirementViewModel vm = Mapper.Map<RequirementEntity, RequirementViewModel>(entity);
                createViewData(vm);

                if (vm == null)
                {
                    return HttpNotFound();
                }
                return View(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RequirementEntity entity = requirementService.get(id);
                RequirementViewModel vm = Mapper.Map<RequirementEntity, RequirementViewModel>(entity);
                if (vm == null)
                {
                    return HttpNotFound();
                }
                return View(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequirementViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RequirementEntity entity = Mapper.Map<RequirementViewModel, RequirementEntity>(vm);
                    setIdUser(ref entity);
                    requirementService.add(ref entity);
                    ViewBag.MessageReqManager = String.Format("Register was made with Success!");
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RequirementViewModel vm)
        {
            try
            {
                RequirementEntity entity = null;

                if (ModelState.IsValid)
                {
                    entity = Mapper.Map<RequirementViewModel, RequirementEntity>(vm);
                    requirementService.update(ref entity, getLoginUser());
                    return RedirectToAction("Index");
                }
                else
                {
                    getModelStateValidations();
                }

                return View(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                requirementService.delete(id);
                requirementService.saveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MessageReqManager = String.Format("Error Detected! " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        #endregion

        #region Private Methods

        private void createViewData(RequirementViewModel vm = null)
        {
            ViewData.Add("RequirementTemplateID", new SelectList(templateService.getAll(), "RequirementTemplateID", "description", vm == null ? 0 : vm.RequirementTemplateID));
            ViewData.Add("StakeholdersProjectID", new SelectList(stakeholdersProject.getAll(), "StakeholdersProjectID", "DisplayName", vm == null ? 0 : vm.StakeholdersProjectID));
            ViewData.Add("MeasureImportanceID", new SelectList(measureService.getAll(), "MeasureImportanceID", "description", vm == null ? 0 : vm.MeasureImportanceID));
            ViewData.Add("RequirementStatusID", new SelectList(statusService.getAll(), "RequirementStatusID", "description", vm == null ? 0 : vm.RequirementStatusID));
            ViewData.Add("RequirementTypeID", new SelectList(typeService.getAll(), "RequirementTypeID", "description", vm == null ? 0 : vm.RequirementTypeID));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name", vm == null ? 0 : vm.UserID));
            ViewData.Add("ProjectID", new SelectList(projectService.getAll(), "ProjectID", "DisplayName", vm == null ? 0 : vm.ProjectID));
        }

        #endregion
    }
}
