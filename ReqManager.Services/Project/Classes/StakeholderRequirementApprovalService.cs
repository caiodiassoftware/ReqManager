using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using System.Linq;

namespace ReqManager.Services.Project.Classes
{
    public class StakeholderRequirementApprovalService : ServiceBase<StakeholderRequirementApproval, StakeholderRequirementApprovalEntity>, IStakeholderRequirementApprovalService
    {
        public StakeholderRequirementApprovalService(IStakeholderRequirementApprovalRepository repository, IUnitOfWork unit) :
            base(repository, unit)
        {
        }

        public StakeholderRequirementApprovalEntity filterByUser(int UserID)
        {
            try
            {
                return getAll().Where(s => s.StakeholderRequirement.StakeholdersProject.Stakeholders.UserID.Equals(UserID)).SingleOrDefault();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
