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
    //REQ3
    public class RequirementController : ControlAcessController<RequirementEntity>
    {
        private IRequirementVersionsService rationaleService { get; set; }
        private ILinkBetweenRequirementsService linkRequirementService { get; set; }
        private ILinkBetweenRequirementsArtifactsService linkReqArtifactService { get; set; }
        private IRequirementService requirementService { get; set; }
        private IProjectService projectService { get; set; }
        private IImportanceService measureService { get; set; }
        private IRequirementStatusService statusService { get; set; }
        private IRequirementTypeService typeService { get; set; }
        private IUserService userService { get; set; }
        private IStakeholdersProjectService stakeholdersProject { get; set; }
        private IRequirementTemplateService templateService { get; set; }
        private IRequirementCharacteristicsService reqCharacteristics { get; set; }
        private IStakeholderRequirementService stakeholdersRequirement { get; set; }
        private IRequirementRequestForChangesService requestService { get; set; }
        private IRequirementSubTypeService subTypeService { get; set; }
        private IRequirementVersionsService versions { get; set; }

        public RequirementController(
            IRequirementService requirementService,
            IImportanceService measureService,
            IRequirementSubTypeService subTypeService,
            IRequirementStatusService statusService,
            IRequirementTypeService typeService,
            IRequirementCharacteristicsService reqCharacteristics,
            IUserService userService,
            IRequirementVersionsService versions,
            IRequirementRequestForChangesService requestService,
            IRequirementVersionsService rationaleService,
            ILinkBetweenRequirementsService linkRequirementService,
            ILinkBetweenRequirementsArtifactsService linkReqArtifactService,
            IStakeholdersProjectService stakeholdersProject,
            IStakeholderRequirementService stakeholdersRequirement,
            IRequirementTemplateService templateService,
            IProjectService projectService) : base(requirementService)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateAutomaticMapping<RequirementViewModel, RequirementEntity>();
                cfg.CreateAutomaticMapping<RequirementEntity, RequirementViewModel>();
            });

            this.versions = versions;
            this.subTypeService = subTypeService;
            this.requestService = requestService;
            this.stakeholdersRequirement = stakeholdersRequirement;
            this.reqCharacteristics = reqCharacteristics;
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
                vm.characteristics = reqCharacteristics.getAll().Where(r => r.RequirementID.Equals(id)).ToList();
                vm.request = requestService.getAll().Where(r => r.RequirementID.Equals(id)).ToList();
                vm.versions = versions.getAll().Where(r => r.RequirementRequestForChanges.RequirementID.Equals(id)).ToList();

                return View(vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Create(int? id)
        {
            try
            {
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                if (projectService.get(Convert.ToInt32(id)) == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                ViewData.Add("RequirementTemplateID", new SelectList(templateService.getAll(), "RequirementTemplateID", "description"));
                ViewData.Add("StakeholdersProjectID", new SelectList(stakeholdersProject.getAll(), "StakeholdersProjectID", "DisplayName"));
                ViewData.Add("ImportanceID", new SelectList(measureService.getAll(), "ImportanceID", "description"));
                ViewData.Add("RequirementStatusID", new SelectList(statusService.getAll(), "RequirementStatusID", "description"));
                ViewData.Add("RequirementTypeID", new SelectList(typeService.getAll(), "RequirementTypeID", "description"));
                ViewData.Add("RequirementSubTypeID", new SelectList(subTypeService.getAll(), "RequirementSubTypeID", "description"));
                ViewData.Add("CreationUserID", new SelectList(userService.getAll(), "UserID", "name"));
                ViewData.Add("ProjectID", new SelectList(projectService.getAll(), "ProjectID", "DisplayName", id == null ? 0 : id));
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

                RequirementRequestForChangesEntity request = requestService.get(id);
                RequirementEditViewModel vm = Mapper.Map<RequirementEntity, RequirementEditViewModel>(request.Requirement);
                vm.RequirementRequestForChangesID = Convert.ToInt32(id);

                ViewData.Add("ImportanceID", new SelectList(measureService.getAll(), "ImportanceID", "description", vm == null ? 0 : vm.ImportanceID));
                ViewData.Add("RequirementStatusID", new SelectList(statusService.getAll(), "RequirementStatusID", "description", vm == null ? 0 : vm.RequirementStatusID));
                ViewData.Add("RequirementTypeID", new SelectList(typeService.getAll(), "RequirementTypeID", "description", vm == null ? 0 : vm.RequirementTypeID));
                ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name", vm == null ? 0 : vm.CreationUserID));
                ViewData.Add("ProjectID", new SelectList(projectService.getAll(), "ProjectID", "DisplayName", vm == null ? 0 : vm.ProjectID));

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
                    Service.add(ref entity);
                }
                return RedirectToAction("Details", "Projects", new { id = vm.ProjectID });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(RequirementEditViewModel vm)
        {
            try
            {

                RequirementEntity req = new RequirementEntity();

                if (ModelState.IsValid)
                {
                    req = Mapper.Map<RequirementEditViewModel, RequirementEntity>(vm);
                    requirementService.update(ref req, vm.RequirementRequestForChangesID, vm.rationale);
                    return RedirectToAction("Index");
                }
                else
                {
                    getModelStateValidations();
                }

                return View(req);
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
    }
}
