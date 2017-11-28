﻿using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using System.Linq;

namespace ReqManager.Services.Project.Classes
{
    public class StakeholderRequirementService : ServiceBase<StakeholderRequirement, StakeholderRequirementEntity>, IStakeholderRequirementService
    {
        public StakeholderRequirementService(IStakeholderRequirementRepository repository, IUnitOfWork unit) :
            base(repository, unit)
        {
        }

        public StakeholderRequirementEntity filterByUser(int UserID)
        {
            try
            {
                return getAll().Where(s => s.StakeholdersProject.Stakeholders.UserID.Equals(UserID)).SingleOrDefault();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public StakeholderRequirementEntity filterByRequirementAndUser(int RequirementID, int UserID)
        {
            try
            {
                return getAll().Where(s => s.StakeholdersProject.Stakeholders.UserID.Equals(UserID) &&
                s. ProjectRequirements.RequirementID.Equals(RequirementID)).SingleOrDefault();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
