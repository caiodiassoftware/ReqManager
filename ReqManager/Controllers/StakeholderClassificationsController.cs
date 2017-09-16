using ReqManager.Services.Project.Interfaces;
using ReqManager.ManagerController;
using ReqManager.Model;

namespace ReqManager.Controllers
{
    public class StakeholderClassificationsController : BaseController<StakeholderClassification>
    {
        public StakeholderClassificationsController(IStakeholderClassificationService service) : base(service)
        {

        }
    }
}
