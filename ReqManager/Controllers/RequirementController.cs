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
using System.Linq;
using System.Net;
using System.Web;
using ReqManager.Services.Documents.Interfaces;
using ReqManager.Entities.Project;
using System.Collections.Generic;

namespace ReqManager.Controllers
{
    //REQ1
    //R-R1
    //R-A1
    //PRJ4
    //REQ3
    //ART1
    public class RequirementController : ControlAccessController<RequirementEntity>
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
        private IStakeholderRequirementApprovalService stakeholdersRequirement { get; set; }
        private IRequirementRequestForChangesService requestService { get; set; }
        private IRequirementSubTypeService subTypeService { get; set; }
        private IRequirementVersionsService versions { get; set; }
        private IRequirementDocumentService reqDocument { get; set; }
        private IStakeholderRequirementApprovalService stakeholderApproval { get; set; }
        IStakeholderRequirementService stakeholderRequirementService { get; set; }

        public RequirementController(
            IRequirementService requirementService,
            IStakeholderRequirementService stakeholderRequirementService,
            IImportanceService measureService,
            IRequirementDocumentService reqDocument,
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
            IStakeholderRequirementApprovalService stakeholdersRequirement,
            IRequirementTemplateService templateService,
            IStakeholderRequirementApprovalService stakeholderApproval,
            IProjectService projectService) : base(requirementService)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateAutomaticMapping<RequirementViewModel, RequirementEntity>();
                cfg.CreateAutomaticMapping<RequirementEntity, RequirementViewModel>();
            });

            this.stakeholderRequirementService = stakeholderRequirementService;
            this.reqDocument = reqDocument;
            this.stakeholderApproval = stakeholderApproval;
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

        public decimal GetRequirementCostByProject(int ProjectID)
        {
            try
            {
                return requirementService.getRequirementCostByProject(ProjectID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetWithCode(string code)
        {
            try
            {
                return Json(requirementService.getWithCode(code),
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PrintDocumentRequirement(int RequirementID)
        {
            try
            {
                RequirementEntity requirement = requirementService.get(RequirementID);
                string title = "ReqManager_" + requirement.code + "_" + DateTime.Now.ToString();

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.ContentType = "application/octet-stream";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.AddHeader("content-disposition", "attachment;filename= " + title + ".pdf");
                Response.Buffer = true;
                Response.Clear();
                var bytes = reqDocument.printRequirement(RequirementID);
                Response.OutputStream.Write(bytes, 0, bytes.Length);
                Response.OutputStream.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Index()
        {
            try
            {
                return View();
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
                vm.request = requestService.getAll().Where(r => r.StakeholderRequirement.RequirementID.Equals(id)).ToList();

                var versionsRequested = versions.getAll().Where(r => r.RequirementRequestForChanges != null).ToList();
                var versionsCreated = versions.getAll().Where(r => r.RequirementRequestForChanges == null).ToList();

                vm.versions = versionsRequested.Where(r => r.RequirementRequestForChanges.StakeholderRequirement.RequirementID.Equals(id)).ToList();
                vm.versions.AddRange(versions.getAll().Where(r => r.RequirementID.Equals(id)));

                vm.stakeholdersApproval = stakeholderApproval.getAll().Where(r => r.StakeholderRequirement.RequirementID.Equals(id)).ToList();
                vm.stakeholders = stakeholderRequirementService.getAll().Where(r => r.RequirementID.Equals(id)).ToList();

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

                ViewData.Add("ProjectID", new SelectList(new List<ProjectEntity>() { projectService.get(id) }, "ProjectID", "DisplayName", id == null ? 0 : id));
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
                RequirementEditViewModel vm = Mapper.Map<RequirementEntity, RequirementEditViewModel>(request.StakeholderRequirement.Requirement);
                vm.RequirementRequestForChangesID = Convert.ToInt32(id);

                if (vm == null)
                {
                    return HttpNotFound();
                }

                ViewData.Add("ImportanceID", new SelectList(measureService.getAll(), "ImportanceID", "description", vm.ImportanceID));
                ViewData.Add("RequirementStatusID", new SelectList(statusService.getAll(), "RequirementStatusID", "description", vm.RequirementStatusID));
                ViewData.Add("RequirementTypeID", new SelectList(typeService.getAll(), "RequirementTypeID", "description", vm.RequirementTypeID));
                ViewData.Add("CreationUserID", new SelectList(userService.getAll(), "UserID", "name", vm.CreationUserID));
                ViewData.Add("ProjectID", new SelectList(projectService.getAll(), "ProjectID", "DisplayName", vm.ProjectID));

                ViewData.Add("RequirementSubTypeID", new SelectList(subTypeService.getAll(), "RequirementSubTypeID", "description",
                    vm.RequirementSubTypeID == null ? 0 : vm.RequirementSubTypeID));
                ViewData.Add("RequirementTemplateID", new SelectList(templateService.getAll(), "RequirementTemplateID", "description",
                    vm.RequirementTemplateID == null ? 0 : vm.RequirementTemplateID));

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
        [AcceptVerbs(HttpVerbs.Post)]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult Create(RequirementViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RequirementEntity entity = Mapper.Map<RequirementViewModel, RequirementEntity>(vm);
                    setIdUser(ref entity);
                    setCreationDate(ref entity);
                    Service.add(ref entity);
                    success("Register was made with Success!");
                    return RedirectToAction("Details", "Requirement", new { id = entity.RequirementID });
                }
                else
                {
                    getModelStateValidations();
                }

                return Create(vm.ProjectID);
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
        public ActionResult Edit(RequirementEditViewModel vm)
        {
            try
            {
                RequirementEntity req = new RequirementEntity();

                if (ModelState.IsValid)
                {
                    req = Mapper.Map<RequirementEditViewModel, RequirementEntity>(vm);
                    requirementService.update(ref req, vm.RequirementRequestForChangesID, vm.rationale);
                    success("Register has been successfully edited!");
                    return RedirectToAction("Details", "Requirement", new { id = req.RequirementID });
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
        [AcceptVerbs(HttpVerbs.Post)]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                requirementService.delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
