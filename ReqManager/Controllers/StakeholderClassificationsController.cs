using ReqManager.Services.Project.Interfaces;
using ReqManager.ManagerController;

namespace ReqManager.Controllers
{
    public class StakeholderClassificationsController : BaseController<Entities.Acess.StakeholderClassificationEntity>
    {
        public StakeholderClassificationsController(IStakeholderClassificationService service) : base(service)
        {

        }
    }
}
