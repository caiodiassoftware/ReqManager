using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Services.Project.Classes
{
    public class ImportanceService : ServiceBase<Importance, ImportanceEntity>, IImportanceService
    {
        public ImportanceService(IImportanceRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
