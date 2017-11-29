using System.Data;

namespace ReqManager.Services.Requirements.Interfaces
{
    public interface IRequirementTraceabilityMatrixService
    {
        string getHtmlMatrix(int ProjectID);
        DataTable getMatrix(int ProjectID);
    }
}
