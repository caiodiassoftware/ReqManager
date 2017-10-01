using ReqManager.Entities.Acess;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Views
{
    public class StakeholderClassificationController : BaseController<StakeholderClassificationEntity>
    {
        public StakeholderClassificationController(IStakeholderClassificationService service) : base(service)
        {
        }
    }
}
