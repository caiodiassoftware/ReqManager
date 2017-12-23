using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;
using System;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementTypeService : ServiceBase<RequirementType, RequirementTypeEntity>, IRequirementTypeService
    {
        private IRequirementSubTypeService subTypeService { get; set; }
        private IRequirementTemplateService templateService { get; set; }

        public RequirementTypeService(
            IRequirementTypeRepository repository,
            IRequirementSubTypeService subTypeService,
            IRequirementTemplateService templateService,
            IUnitOfWork unit) : base(repository, unit)
        {
            this.templateService = templateService;
            this.subTypeService = subTypeService;
        }
    }

}
