using ReqManager.Entities;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Services.InterfacesServices;

namespace ReqManager.Controllers
{
    public class ControllerActionController : BaseController<ControllerActionEntity>
    {
        public ControllerActionController(IControllerActionService service) : base(service)
        {

        }
    }
}
