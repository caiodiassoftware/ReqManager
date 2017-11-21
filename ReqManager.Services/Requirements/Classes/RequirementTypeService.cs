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

        public override void add(ref RequirementTypeEntity entity, bool persistir = true)
        {
            try
            {
                unit.BeginTransaction();
                base.add(ref entity, false);

                RequirementSubTypeEntity subType = new RequirementSubTypeEntity();
                subType.description = entity.description + " Doesn't Have SubType";
                subType.RequirementTypeID = entity.RequirementTypeID;
                subTypeService.add(ref subType, false);

                //RequirementTemplateEntity template = new RequirementTemplateEntity();
                //template.description = entity.description + " Doesn't Have Template";
                //template.RequirementTypeID = entity.RequirementTypeID;
                //template.templateHtml = "Insert your text";
                //templateService.add(ref template, false);

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
