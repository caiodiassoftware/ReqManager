using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReqManager.Services.Project.Classes
{
    public class StakeholdersProjectService : ServiceBase<StakeholdersProject, StakeholdersProjectEntity>, 
        IStakeholdersProjectService
    {
        public StakeholdersProjectService(IStakeholdersProjectRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }

        public StakeholdersProjectEntity getByRequirementAndUser(int ProjectID, int UserID)
        {
            try
            {
                return getAll().Where(p => p.ProjectID.Equals(ProjectID) && p.Stakeholders.UserID.Equals(UserID)).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<StakeholdersProjectEntity> getStakeholderByProject(int ProjectID)
        {
            try
            {
                return getAll().Where(p => p.ProjectID.Equals(ProjectID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
