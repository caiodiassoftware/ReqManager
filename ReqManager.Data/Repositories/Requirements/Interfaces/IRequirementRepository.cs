using ReqManager.Data.Infrastructure;
using ReqManager.Model;
using System.Data;

namespace ReqManager.Data.Repositories.Requirements.Interfaces
{
    public interface IRequirementRepository : IRepository<Requirement>
    {
        DataTable getDataSetRequirementPreconditions(int ProjectID = 0);
        DataTable getDataSetRequirementPreconditionsAndImportanceAndCost(int ProjectID = 0);
        DataTable getDataSetRequirementImportanceAndCost(int ProjectID = 0);
    }
}
