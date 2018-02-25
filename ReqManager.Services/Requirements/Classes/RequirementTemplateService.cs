using System;
using System.Collections.Generic;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;
using System.Linq;
using AutoMapper;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementTemplateService : 
        ServiceBase<RequirementTemplate, RequirementTemplateEntity>,
        IRequirementTemplateService
    {
        private IRequirementTemplateRepository repository { get; set; }

        public RequirementTemplateService(IRequirementTemplateRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
            this.repository = repository;
        }

        public List<RequirementTemplateEntity> filterByRequirementType(int RequirementType)
        {
            try
            {
                return convertEnumerableModelToEntity(repository.filter(r => r.RequirementTypeID == RequirementType)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
