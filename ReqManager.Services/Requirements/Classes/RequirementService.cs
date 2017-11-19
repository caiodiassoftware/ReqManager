using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementService : ServiceBase<Requirement, RequirementEntity>, IRequirementService
    {
        public RequirementService(
            IRequirementRepository repository,
            IUnitOfWork unit) : base(repository, unit)
        {

        }

        public void update(ref RequirementEntity entity, string userLogin)
        {
            try
            {
                unit.BeginTransaction();

                update(ref entity, false);

                unit.Commit();
            }
            catch (System.Exception ex)
            {
                unit.Rollback();
                throw ex;
            }
        }
    }
}
