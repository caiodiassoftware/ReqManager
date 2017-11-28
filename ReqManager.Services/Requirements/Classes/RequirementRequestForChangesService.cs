using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;
using System.Linq;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementRequestForChangesService :
        ServiceBase<RequirementRequestForChanges, RequirementRequestForChangesEntity>, IRequirementRequestForChangesService
    {
        public RequirementRequestForChangesService(IRequirementRequestForChangesRepository repository, IUnitOfWork unit) : 
            base(repository, unit)
        {
        }

        public bool validateRequestForRequirement(int RequirementID)
        {
            try
            {
                var request = getAll().Where(r => r.RequirementID.Equals(RequirementID) && r.RequestStatusID.Equals(1)).SingleOrDefault();
                return request == null ? true : false;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }

}
