using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using System.Linq;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace ReqManager.Services.Project.Classes
{
    public class StakeholderRequirementApprovalService : ServiceBase<StakeholderRequirementApproval, 
        StakeholderRequirementApprovalEntity>, IStakeholderRequirementApprovalService
    {
        private IStakeholderRequirementApprovalRepository repository { get; set; }

        public StakeholderRequirementApprovalService(IStakeholderRequirementApprovalRepository repository, IUnitOfWork unit) :
            base(repository, unit)
        {
            this.repository = repository;
        }

        public List<StakeholderRequirementApprovalEntity> filterByRequirement(int RequirementID)
        {
            try
            {
                return convertEnumerableModelToEntity(repository.filterByRequirement(RequirementID)).ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public StakeholderRequirementApprovalEntity filterByUser(int UserID)
        {
            try
            {
                return convertModelToEntity(repository.filter(s => s.StakeholderRequirement.StakeholdersProject.Stakeholders.UserID.Equals(UserID)).SingleOrDefault());
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
