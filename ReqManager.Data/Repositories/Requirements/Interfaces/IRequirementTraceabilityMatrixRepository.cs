using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Repositories.Requirements.Interfaces
{
    public interface IRequirementTraceabilityMatrixRepository
    {
        DataTable getMatrix();
    }
}
