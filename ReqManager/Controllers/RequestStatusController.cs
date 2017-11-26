using ReqManager.Entities.Requirement;
using ReqManager.ManagerController;
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Controllers
{
    public class RequestStatusController : BaseController<RequestStatusEntity>
    {
        public RequestStatusController(IRequestStatusService service) : base(service)
        {
        }
    }
}
