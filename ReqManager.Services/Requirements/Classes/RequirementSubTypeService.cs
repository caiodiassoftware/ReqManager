using System;
using System.Collections.Generic;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;
using AutoMapper;
using System.Linq;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementSubTypeServiceService : 
        ServiceBase<RequirementSubType, RequirementSubTypeEntity>, IRequirementSubTypeService
    {
        private IRequirementSubTypeRepository repository { get; set; }

        public RequirementSubTypeServiceService(IRequirementSubTypeRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
            this.repository = repository;
        }

        public List<RequirementSubTypeEntity> filterByRequirementType(int RequirementTypeID)
        {
            return Mapper.Map<IEnumerable<RequirementSubType>, 
                IEnumerable<RequirementSubTypeEntity>>(repository.filterByRequirementType(RequirementTypeID)).ToList();
        }
    }
}
