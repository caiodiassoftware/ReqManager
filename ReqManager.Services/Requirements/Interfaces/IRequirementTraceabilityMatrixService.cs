using System.Data;

namespace ReqManager.Services.Requirements.Interfaces
{
    public interface IRequirementTraceabilityMatrixService
    {
        DataTable getMatrix(int ProjectID);
    }
}
