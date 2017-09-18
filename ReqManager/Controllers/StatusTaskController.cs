using ReqManager.Services.Task.Interfaces;
using ReqManager.ManagerController;

namespace ReqManager.Controllers
{
    public class StatusTaskController : BaseController<Entities.Task.StatusTaskEntity>
    {
        public StatusTaskController(IStatusTaskService service) : base(service)
        {

        }
    }
}
