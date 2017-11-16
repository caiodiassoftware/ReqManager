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
        private IRequirementActionHistoryService reqActionHistoryService { get; set; }

        public RequirementService(
            IRequirementRepository repository,
            IRequirementActionHistoryService reqActionHistoryService,
            IUnitOfWork unit) : base(repository, unit)
        {
            this.reqActionHistoryService = reqActionHistoryService;
        }

        public void update(ref RequirementEntity entity, string userLogin)
        {
            try
            {
                unit.BeginTransaction();

                update(ref entity, false);
                RequirementActionHistoryEntity reqAction = new RequirementActionHistoryEntity();
                reqAction.RequirementID = entity.RequirementID;
                reqAction.DescriptionStatus = entity.RequirementStatus.description;
                reqAction.UserLogin = userLogin;

                reqActionHistoryService.add(ref reqAction, false);

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
