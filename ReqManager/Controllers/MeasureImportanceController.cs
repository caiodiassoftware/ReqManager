using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Controllers
{
    public class MeasureImportanceController : BaseController<MeasureImportanceEntity>
    {
        public MeasureImportanceController(IMeasureImportanceService service) : base(service)
        {
        }
    }
}
