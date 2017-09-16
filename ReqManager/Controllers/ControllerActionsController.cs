using ReqManager.Model;
using ReqManager.ManagerController;
using ReqManager.Services.Acess;

namespace ReqManager.Controllers
{
    public class ControllerActionsController : BaseController<ControllerAction>
    {
        public ControllerActionsController(ControllerActionService service) : base(service)
        {

        }
    }
}
