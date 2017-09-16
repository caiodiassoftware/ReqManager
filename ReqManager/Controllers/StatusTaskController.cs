using ReqManager.Model;
using ReqManager.Services.Task.Interfaces;
using ReqManager.ManagerController;

namespace ReqManager.Controllers
{
    public class StatusTaskController : BaseController<StatusTask>
    {
        public StatusTaskController(IStatusTaskService service) : base(service)
        {

        }
    }
}
