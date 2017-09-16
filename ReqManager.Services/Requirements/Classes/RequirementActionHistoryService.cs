using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Services.Requirements.Classes
{

    public class RequirementActionHistoryService : ServiceBase<RequirementActionHistory>, IRequirementActionHistoryService
    {
        public RequirementActionHistoryService(IRequirementActionHistoryRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
