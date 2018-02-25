using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReqManager.Services.Project.Classes
{
    public class StakeholderRequirementService : 
        ServiceBase<StakeholderRequirement, StakeholderRequirementEntity>, IStakeholderRequirementService
    {
        private IRequirementService requirement { get; set; }
        private IStakeholdersProjectService stakeholderProjectService { get; set; }

        public StakeholderRequirementService(
            IRequirementService requirement,
            IStakeholdersProjectService stakeholderProjectService,
            IStakeholderRequirementRepository repository, IUnitOfWork unit) : 
            base(repository, unit)
        {
            this.requirement = requirement;
            this.stakeholderProjectService = stakeholderProjectService;
        }

        public IEnumerable<StakeholderRequirementEntity> getStakeholdersFromRequirement(int RequirementID)
        {
            try
            {
                return convertEnumerableModelToEntity(repository.filter(r => r.RequirementID == RequirementID));
            }
            catch (Exception ex)

            {
                throw ex;
            }
        }

        public StakeholderRequirementEntity get(int RequirementID, int UserID)
        {
            try
            {
                RequirementEntity req = requirement.get(RequirementID);
                StakeholdersProjectEntity stakeProject = stakeholderProjectService.getByProjectAndUser(req.ProjectID, UserID);

                return convertModelToEntity(repository.filter(s => s.RequirementID == RequirementID &&
                s.StakeholdersProject.StakeholdersProjectID == stakeProject.StakeholdersProjectID).SingleOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
