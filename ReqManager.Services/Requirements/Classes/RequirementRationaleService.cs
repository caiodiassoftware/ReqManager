using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementRationaleService : ServiceBase<RequirementVersions, RequirementVersionsEntity>, IRequirementVersionsService
    {
        public RequirementRationaleService(IRequirementRationaleRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
