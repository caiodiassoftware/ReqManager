using ReqManager.Data.Infrastructure;
using ReqManager.Model;
using System.Collections.Generic;
namespace ReqManager.Data.Repositories.Requirements.Interfaces
{
    public interface IRequirementTemplateRepository : IRepository<RequirementTemplate>
    {
        List<RequirementTemplate> filterByRequirementType(int RequirementType);
    }
}
