using System.Data;
namespace ReqManager.Services.Artifact.Interfaces
{
    public interface IArtifactRequirementTraceabilityMatrixService
    {
        DataTable getMatrix(int ProjectID);
    }
}
