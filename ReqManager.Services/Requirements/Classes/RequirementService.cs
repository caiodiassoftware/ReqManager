using AutoMapper;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Extensions;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using System;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementService : ServiceBase<Requirement, RequirementEntity>, IRequirementService
    {
        private IRequirementVersionsService versionService { get; set; }
        private IRequirementRequestForChangesService requestService { get; set; }
        private IProjectRequirementsService projectRequirementService { get; set; }

        public RequirementService(
            IRequirementRepository repository,
            IRequirementRequestForChangesService requestService,
            IRequirementVersionsService versionService,
            IProjectRequirementsService projectRequirementService,
            IUnitOfWork unit) : base(repository, unit)
        {
            this.projectRequirementService = projectRequirementService;
            this.requestService = requestService;
            this.versionService = versionService;
        }

        public void add(ref RequirementEntity requirement, 
            ref ProjectRequirementsEntity projectRequirement)
        {
            try
            {
                BeginTransaction();
                requirement.versionNumber = 1;
                base.add(ref requirement, false);

                projectRequirement.RequirementID = requirement.RequirementID;

                projectRequirementService.add(ref projectRequirement, false);
                Commit();
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
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
