using AutoMapper;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Extensions;
using ReqManager.Services.Requirements.Interfaces;
using System;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementService : ServiceBase<Requirement, RequirementEntity>, IRequirementService
    {
        private IRequirementVersionsService versionService { get; set; }
        private IRequirementRequestForChangesService requestService { get; set; }

        public RequirementService(
            IRequirementRepository repository,
            IRequirementRequestForChangesService requestService,
            IRequirementVersionsService versionService,
            IUnitOfWork unit) : base(repository, unit)
        {
            this.requestService = requestService;
            this.versionService = versionService;
        }

        public override void add(ref RequirementEntity entity, bool persistir = true)
        {
            entity.versionNumber = 1;
            base.add(ref entity, persistir);
        }

        public void update(ref RequirementEntity entity, int RequirementRequestForChangesID, string rationale)
        {
            try
            {
                unit.BeginTransaction();

                Mapper.Initialize(cfg =>
                {
                    cfg.CreateAutomaticMapping<RequirementEntity, RequirementVersionsEntity>();
                });

                RequirementRequestForChangesEntity request = requestService.get(RequirementRequestForChangesID);

                //TODO
                request.RequestStatusID = 3;
                request.RequestStatus.RequestStatusID = 3;

                RequirementVersionsEntity version = new RequirementVersionsEntity();
                version = Mapper.Map<RequirementEntity, RequirementVersionsEntity>(entity);
                version.versionNumber = entity.versionNumber + 1;
                version.creationDate = DateTime.Now;
                version.rationale = rationale;
                version.RequirementSubTypeID = 2;
                version.RequirementRequestForChangesID = request.RequirementRequestForChangesID;

                versionService.add(ref version, false);
                requestService.update(ref request, false);
                update(ref entity, false);

                unit.Commit();
            }
            catch (Exception ex)
            {
                unit.Rollback();
                throw ex;
            }
        }
    }
}
