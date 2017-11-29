using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Data;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Utils.Extensions;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementTraceabilityMatrixService : IRequirementTraceabilityMatrixService
    {
        private IRequirementTraceabilityMatrixRepository repository { get; set; }

        public RequirementTraceabilityMatrixService(IRequirementTraceabilityMatrixRepository repository)
        {
            this.repository = repository;
        }

        public string getHtmlMatrix(int ProjectID)
        {
            return repository.getMatrix(ProjectID).ConvertDataTableToHTML();
        }

        public DataTable getMatrix(int ProjectID)
        {
            return repository.getMatrix(ProjectID);
        }

        public DataTable getMatrixByProject(int ProjectID)
        {
            try
            {
                return getMatrix(ProjectID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
