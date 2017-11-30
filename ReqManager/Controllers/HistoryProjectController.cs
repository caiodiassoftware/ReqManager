using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class HistoryProjectController : BaseController<HistoryProjectEntity>
    {
        public HistoryProjectController(
            IHistoryProjectService historyProjectService,
            IProjectService projectService,
            IUserService userService) : base(historyProjectService)
        {

        }        
    }
}
