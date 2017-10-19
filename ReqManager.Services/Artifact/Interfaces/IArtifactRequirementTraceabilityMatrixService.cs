using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Artifact.Interfaces
{
    public interface IArtifactRequirementTraceabilityMatrixService
    {
        DataTable getMatrix();
    }
}
