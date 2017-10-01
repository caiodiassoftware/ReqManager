using System;
using System.Web.Mvc;
using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace ReqManager.Controllers
{
    public class ProjectsController : BaseController<ProjectEntity>
    {
        private IHistoryProjectService historyProjectService { get; set; }

        public ProjectsController(
            IProjectService service,
            IUserService userService,
            IProjectPhasesService phasesService,
            IHistoryProjectService historyProjectService) : base(service)
        {
            this.historyProjectService = historyProjectService;

            ViewData.Add("ProjectPhasesID", new SelectList(phasesService.getAll(), "ProjectPhasesID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(ProjectEntity entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.update(entity);

                    HistoryProjectEntity history = new HistoryProjectEntity();
                    ProjectEntity project = Service.get(entity.ProjectID);
                    history.ProjectID = project.ProjectID;
                    history.UserID = getIdUser();
                    history.descriptionPhases = project.ProjectPhases.description;
                    history.endDate = Convert.ToDateTime(project.endDate);
                    history.startDate = Convert.ToDateTime(project.startDate);

                    historyProjectService.add(history);

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
