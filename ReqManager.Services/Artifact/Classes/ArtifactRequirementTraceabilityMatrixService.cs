using ReqManager.Data.Repositories.Artifact.Interfaces;
using ReqManager.Services.Artifact.Interfaces;
using System.Data;

namespace ReqManager.Services.Artifact.Classes
{
    public class ArtifactRequirementTraceabilityMatrixService : IArtifactRequirementTraceabilityMatrixService
    {
        private IArtifactRequirementTraceabilityMatrixRepository matrix { get; set; }

        public ArtifactRequirementTraceabilityMatrixService(IArtifactRequirementTraceabilityMatrixRepository matrix)
        {
            this.matrix = matrix;
        }

        public DataTable getMatrix(int ProjectID)
        {
            return matrix.getMatrix(ProjectID);
        }
    }
}
