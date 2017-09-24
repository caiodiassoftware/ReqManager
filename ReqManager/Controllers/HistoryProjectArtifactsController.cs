using System.Web.Mvc;
using ReqManager.Entities.Artifact;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Controllers
{
    public class HistoryProjectArtifactsController : BaseController<HistoryProjectArtifactEntity>
    {
        private IHistoryProjectArtifactService service { get; set; }
        private IProjectArtifactService projectArtifactService { get; set; }

        public HistoryProjectArtifactsController(IHistoryProjectArtifactService service, IProjectArtifactService projectArtifactService) : base(service)
        {
            this.service = service;
            this.projectArtifactService = projectArtifactService;
        }

        #region Private Methods

        private ActionResult dropDowns(HistoryProjectArtifactEntity entity = null)
        {
            ViewBag.ProjectArtifactID = new SelectList(projectArtifactService.getAll(), "ProjectArtifactID", "code");
            return entity == null ? View() : View(entity);
        }

        #endregion
    }
}
