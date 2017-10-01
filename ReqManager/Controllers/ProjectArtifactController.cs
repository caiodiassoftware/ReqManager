using System;
using System.Web.Mvc;
using ReqManager.Entities.Artifact;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace ReqManager.Controllers
{
    public class ProjectArtifactController : BaseController<ProjectArtifactEntity>
    {
        private IHistoryProjectArtifactService historyServiceArtifact { get; set; }

        public ProjectArtifactController(
            IProjectArtifactService service,
            IUserService userService,
            IArtifactTypeService typeService,
            IMeasureImportanceService measureService,
            IProjectService projectService,
            IHistoryProjectArtifactService historyServiceArtifact) : base(service)
        {
            this.historyServiceArtifact = historyServiceArtifact;

            ViewData.Add("ArtifactTypeID", new SelectList(typeService.getAll(), "ArtifactTypeID", "description"));
            ViewData.Add("MeasureImportanceID", new SelectList(measureService.getAll(), "MeasureImportanceID", "description"));
            ViewData.Add("ProjectID", new SelectList(projectService.getAll(), "ProjectID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(ProjectArtifactEntity entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.update(entity);

                    HistoryProjectArtifactEntity history = new HistoryProjectArtifactEntity();
                    ProjectArtifactEntity artifact = Service.get(entity.ProjectArtifactID);
                    history.ProjectArtifactID = artifact.ProjectArtifactID;
                    history.description = artifact.description;
                    history.descriptionMeasureImportance = artifact.MeasureImportance.description;
                    history.descriptionTypeArtifact = artifact.ArtifactType.description;
                    history.login = getLoginUser();
                    history.path = artifact.path;

                    historyServiceArtifact.add(history);

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
