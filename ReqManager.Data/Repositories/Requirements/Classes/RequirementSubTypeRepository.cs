using System;
using System.Collections.Generic;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Model;
using System.Linq;

namespace ReqManager.Data.Repositories.Requirements.Classes
{
    public class RequirementSubTypeRepository : RepositoryBase<RequirementSubType>, IRequirementSubTypeRepository
    {
        public RequirementSubTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public List<RequirementSubType> filterByRequirementType(int RequirementTypeID)
        {
            return DbContext.RequirementSubType.Where(s => s.RequirementTypeID == RequirementTypeID).ToList();
        }
    }
}
