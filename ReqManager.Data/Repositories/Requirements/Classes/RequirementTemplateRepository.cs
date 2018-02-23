using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Model;
using System.Collections.Generic;
using System.Linq;

namespace ReqManager.Data.Repositories.Requirements.Classes
{
    public class RequirementTemplateRepository : RepositoryBase<RequirementTemplate>, IRequirementTemplateRepository
    {
        public RequirementTemplateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public List<RequirementTemplate> filterByRequirementType(int RequirementType)
        {
            return DbContext.RequirementTemplate.Where(t => t.RequirementTypeID == RequirementType).ToList();
        }
    }
}
