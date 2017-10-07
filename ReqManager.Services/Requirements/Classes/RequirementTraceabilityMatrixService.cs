using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public string getHtmlMatrix()
        {
            return repository.getMatrix().ConvertDataTableToHTML();
        }

        public DataTable getMatrix()
        {
            return repository.getMatrix();
        }
    }
}
