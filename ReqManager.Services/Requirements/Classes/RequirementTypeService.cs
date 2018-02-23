using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementTypeService : ServiceBase<RequirementType, RequirementTypeEntity>, IRequirementTypeService
    {
        public RequirementTypeService(
            IRequirementTypeRepository repository,
            IRequirementSubTypeService subTypeService,
            IRequirementTemplateService templateService,
            IUnitOfWork unit) : base(repository, unit)
        {
        }
    }

}
