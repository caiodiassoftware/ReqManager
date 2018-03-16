using ReqManager.Data.Infrastructure;
using ReqManager.Model;
using System.Data;

namespace ReqManager.Data.Repositories.Requirements.Interfaces
{
    public interface IRequirementRepository : IRepository<Requirement>
    {
        DataTable DataSetDependencies(int ProjectID = 0);
        DataTable DataSetPriorities(int ProjectID = 0);
        DataTable DataSetRequirementsCost(int ProjectID = 0);
        DataTable DataSetStakeholderImportances(int ProjectID = 0);
    }
}
