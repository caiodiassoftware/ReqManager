using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Entities.Requirement;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;
using System.Linq;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace ReqManager.Services.Requirements.Classes
{
    public class RequirementRequestForChangesService :
        ServiceBase<RequirementRequestForChanges, RequirementRequestForChangesEntity>, IRequirementRequestForChangesService
    {
        private IRequirementRequestForChangesRepository repository { get; set; }

        public RequirementRequestForChangesService(IRequirementRequestForChangesRepository repository, IUnitOfWork unit) : 
            base(repository, unit)
        {
            this.repository = repository;
        }

        public List<RequirementRequestForChangesEntity> filterByRequirement(int RequirementID)
        {
            return convertEnumerableModelToEntity(repository.filterByRequirement(RequirementID)).ToList();
        }

        public bool validateRequestForRequirement(int RequirementID)
        {
            try
            {
                return !repository.filter(r => 
                    r.StakeholderRequirement.RequirementID.Equals(RequirementID)
                    && r.RequestStatusID.Equals(1)).Any();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }

}
