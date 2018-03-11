using ReqManager.Entities.Requirement;
using ReqManager.Services.Estructure;
using System.Collections.Generic;
using System.Data;

namespace ReqManager.Services.Requirements.Interfaces
{
    public interface IRequirementService : IService<RequirementEntity>
    {
        void update(ref RequirementEntity entity, int RequirementRequestForChangesID, string rationale);
        IEnumerable<RequirementEntity> getRequirementsByProject(int ProjectID);
        IEnumerable<RequirementEntity> getRequirementsToLink(int RequirementID);
        RequirementEntity getWithCode(string code);
        decimal getRequirementCostByProject(int ProjectID);
        DataTable getDataSetRequirementPreconditions(int ProjectID = 0);
        DataTable getDataSetRequirementPreconditionsAndImportanceAndCost(int ProjectID = 0);
        DataTable getDataSetRequirementImportanceAndCost(int ProjectID = 0);
    }
}
