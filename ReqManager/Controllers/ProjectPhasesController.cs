using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Controllers
{
    public class ProjectPhasesController : BaseController<ProjectPhasesEntity>
    {
        public ProjectPhasesController(IProjectPhasesService service) : base(service)
        {
        }
    }
}
