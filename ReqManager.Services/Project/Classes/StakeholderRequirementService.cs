using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Linq;

namespace ReqManager.Services.Project.Classes
{
    public class StakeholderRequirementService : 
        ServiceBase<StakeholderRequirement, StakeholderRequirementEntity>, IStakeholderRequirementService
    {
        public StakeholderRequirementService(IStakeholderRequirementRepository repository, IUnitOfWork unit) : 
            base(repository, unit)
        {
        }

        public StakeholderRequirementEntity get(int UserID, int RequirementID)
        {
            try
            {
                return getAll().Where(s => s.RequirementID.Equals(RequirementID) && s.StakeholdersProject.Stakeholders.UserID.Equals(UserID)).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
