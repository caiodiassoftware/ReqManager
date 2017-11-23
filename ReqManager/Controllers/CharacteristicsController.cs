using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;

namespace ReqManager.Controllers
{
    public class CharacteristicsController : BaseController<CharacteristicsEntity>
    {
        public CharacteristicsController(IService<CharacteristicsEntity> service) : base(service)
        {
        }
    }
}
