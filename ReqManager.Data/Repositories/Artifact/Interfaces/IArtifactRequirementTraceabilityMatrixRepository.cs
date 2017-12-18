using System.Data;

namespace ReqManager.Data.Repositories.Artifact.Interfaces
{
    public interface IArtifactRequirementTraceabilityMatrixRepository
    {
        DataTable getMatrix(int ProjectID);
    }
}
