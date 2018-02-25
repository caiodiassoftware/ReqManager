using ReqManager.Services.Requirements.Interfaces;
using System.Data;
using ReqManager.Data.Repositories.Requirements.Interfaces;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementTraceabilityMatrixService : IRequirementTraceabilityMatrixService
    {
        private IRequirementTraceabilityMatrixRepository repository { get; set; }

        public RequirementTraceabilityMatrixService(IRequirementTraceabilityMatrixRepository repository)
        {
            this.repository = repository;
        }

        public DataTable getMatrix(int ProjectID)
        {
            return repository.getMatrix(ProjectID);
        }
    }
}
