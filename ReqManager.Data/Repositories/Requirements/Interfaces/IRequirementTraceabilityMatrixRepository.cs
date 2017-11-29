using System.Data;

namespace ReqManager.Data.Repositories.Requirements.Interfaces
{
    public interface IRequirementTraceabilityMatrixRepository
    {
        DataTable getMatrix(int ProjectID);
    }
}
