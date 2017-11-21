using ReqManager.Entities.Project;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Controllers
{
    public class ImportanceController : BaseController<ImportanceEntity>
    {
        public ImportanceController(IImportanceService service) : base(service)
        {
        }
    }
}
