using System;
using System.Web.Mvc;
using ReqManager.Entities.Artifact;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ReqManager.Controllers
{
    //ART2
    public class ProjectArtifactController : BaseController<ProjectArtifactEntity>
    {
        private IHistoryProjectArtifactService historyServiceArtifact { get; set; }

        public ProjectArtifactController(
            IProjectArtifactService service,
            IUserService userService,
            IArtifactTypeService typeService,
            IImportanceService measureService,
            IProjectService projectService,
            IHistoryProjectArtifactService historyServiceArtifact) : base(service)
        {
            this.historyServiceArtifact = historyServiceArtifact;

            ViewData.Add("ArtifactTypeID", new SelectList(typeService.getAll(), "ArtifactTypeID", "description"));
            ViewData.Add("MeasureImportanceID", new SelectList(measureService.getAll(), "MeasureImportanceID", "description"));
            ViewData.Add("ProjectID", new SelectList(projectService.getAll(), "ProjectID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        public JsonResult GetArtifactsFromProject(int ProjectID)
        {
            try
            {
                return Json(Service.getAll().Where(a => a.ProjectID.Equals(ProjectID)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(ProjectArtifactEntity entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.update(ref entity);

                    HistoryProjectArtifactEntity history = new HistoryProjectArtifactEntity();
                    ProjectArtifactEntity artifact = Service.get(entity.ProjectArtifactID);
                    history.ProjectArtifactID = artifact.ProjectArtifactID;
                    history.description = artifact.description;
                    history.descriptionImportance = artifact.Importance.description;
                    history.descriptionTypeArtifact = artifact.ArtifactType.description;
                    history.login = getLoginUser();
                    history.path = artifact.path;

                    historyServiceArtifact.add(ref history);

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
