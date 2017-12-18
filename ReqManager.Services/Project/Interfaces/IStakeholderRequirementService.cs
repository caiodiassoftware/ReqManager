﻿using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;

namespace ReqManager.Services.Project.Interfaces
{
    public interface IStakeholderRequirementService : IService<StakeholderRequirementEntity>
    {
        StakeholderRequirementEntity get(int UserID, int RequirementID);
    }
}