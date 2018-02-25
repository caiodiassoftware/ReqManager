using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReqManager.Data.Repositories.Project.Classes
{
    public class StakeholdersProjectRepository : RepositoryBase<StakeholdersProject>, IStakeholdersProjectRepository
    {
        public StakeholdersProjectRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<StakeholdersProject> getStakeholderByProject(int ProjectID)
        {
            try
            {
                return DbContext.StakeholdersProject.AsNoTracking().Where(p => p.ProjectID == ProjectID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
