using System;
using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using ReqManager.Services.Directories.Interfaces;
using ReqManager.ViewModels;
using ReqManager.Services.Requirements.Interfaces;
using ReqManager.Services.Documents.Interfaces;
using System.Web;
using ReqManager.Services.Artifact.Interfaces;
using System.Linq;

namespace ReqManager.Controllers
{
    public class ProjectsController : BaseController<ProjectEntity>
    {
        private IHistoryProjectService historyProjectService { get; set; }
        private IScanDirectoryService directory { get; set; }
        private IProjectService projectService { get; set; }
        private IProjectArtifactService projectArtifact { get; set; }
        private IStakeholdersProjectService stakeholders { get; set; }
        private IRequirementTraceabilityMatrixService matrixService { get; set; }
        private IRequirementService requirementService { get; set; }
        private IRequirementDocumentService reqDocument { get; set; }
        private IArtifactRequirementTraceabilityMatrixService artifactMatrix {get;set;}

        public ProjectsController(
            IProjectService projectService,
            IUserService userService,
            IArtifactRequirementTraceabilityMatrixService artifactMatrix,
            IRequirementService requirementService,
            IStakeholdersProjectService stakeholders,
            IProjectArtifactService projectArtifact,
            IProjectPhasesService phasesService,
            IHistoryProjectService historyProjectService,
            IRequirementTraceabilityMatrixService matrixService,
            IScanDirectoryService directory,
            IRequirementDocumentService reqDocument) : base(projectService)
        {
            this.artifactMatrix = artifactMatrix;
            this.reqDocument = reqDocument;
            this.requirementService = requirementService;
            this.matrixService = matrixService;
            this.stakeholders = stakeholders;
            this.projectArtifact = projectArtifact;
            this.historyProjectService = historyProjectService;
            this.directory = directory;
            this.projectService = projectService;

            ViewData.Add("ProjectPhasesID", new SelectList(phasesService.getAll(), "ProjectPhasesID", "description"));
            ViewData.Add("CreationUserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        public void PrintDocumentRequirement(int ProjectID, int RequirementTypeID)
        {
            try
            {
                ProjectEntity project = projectService.get(ProjectID);
                string title = "ReqManager_" + project.code + "_" + DateTime.Now.ToString();

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.ContentType = "application/octet-stream";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.AddHeader("content-disposition", "attachment;filename= " + title + ".pdf");
                Response.Buffer = true;
                Response.Clear();
                var bytes = reqDocument.printDocumentRequirementProject(ProjectID, RequirementTypeID);
                Response.OutputStream.Write(bytes, 0, bytes.Length);
                Response.OutputStream.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetRequirementsFromProject(int ProjectID)
        {
            try
            {
                var json = requirementService.getRequirementsByProject(ProjectID);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetFolders(int ProjectID)
        {
            try
            {
                ProjectEntity prj = Service.get(ProjectID);
                var path = directory.getFolders(prj.pathForTraceability);
                return Json(path, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override ActionResult Details(int? id)
        {
            try
            {
                ProjectDetailsViewModel prj = new ProjectDetailsViewModel();

                int ProjectID = Convert.ToInt32(id);

                prj.project = projectService.get(id);
                prj.stakeholders = stakeholders.getStakeholderByProject(ProjectID);
                prj.artifacts = projectArtifact.getArtifactsByProject(ProjectID);
                prj.requirements = requirementService.getRequirementsByProject(ProjectID);
                prj.requirementMatrix = matrixService.getMatrix(ProjectID);
                prj.artifactMatrix = artifactMatrix.getMatrix(ProjectID);
                prj.history = historyProjectService.getProjectHistory(ProjectID);

                return View(prj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public override ActionResult Edit(ProjectEntity entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    projectService.update(ref entity, getIdUser());
                    success("Register has been successfully edited!");
                    return RedirectToAction("Details", "Projects", new { id = entity.ProjectID});
                }
                else
                {
                    getModelStateValidations();
                }

                return View(entity);
            }
            catch (Exception ex)
            {
                return filterException(ex);
            }
        }
    }
}
