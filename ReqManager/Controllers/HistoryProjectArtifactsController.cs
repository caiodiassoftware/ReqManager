using System.Web.Mvc;
using ReqManager.Entities.Artifact;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Controllers
{
    public class HistoryProjectArtifactsController : BaseController<HistoryProjectArtifactEntity>
    {
        public HistoryProjectArtifactsController(IHistoryProjectArtifactService service, IProjectArtifactService projectArtifactService) : base(service)
        {
            ViewData.Add("ProjectArtifactID", new SelectList(projectArtifactService.getAll(), "ProjectArtifactID", "code"));
        }
    }
}
