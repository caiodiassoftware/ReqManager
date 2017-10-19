using ReqManager.Data.Repositories.Artifact.Interfaces;
using ReqManager.Services.Artifact.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Artifact.Classes
{
    public class ArtifactRequirementTraceabilityMatrixService : IArtifactRequirementTraceabilityMatrixService
    {
        private IArtifactRequirementTraceabilityMatrixRepository matrix { get; set; }

        public ArtifactRequirementTraceabilityMatrixService(IArtifactRequirementTraceabilityMatrixRepository matrix)
        {
            this.matrix = matrix;
        }

        public DataTable getMatrix()
        {
            return matrix.getMatrix();
        }
    }
}
