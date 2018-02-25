using System;
using System.Collections.Generic;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Link.Interfaces;
using ReqManager.Entities.Link;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Link.Interfaces;
using System.Linq;

namespace ReqManager.Services.Link.Classes
{
    public class LinkBetweenRequirementsService : 
        ServiceBase<LinkBetweenRequirement, LinkBetweenRequirementsEntity>, 
        ILinkBetweenRequirementsService
    {
        private ILinkRequirementAttributesService requirementsAttributes { get; set; }

        public LinkBetweenRequirementsService(
            ILinkBetweenRequirementRepository repository,
            ILinkRequirementAttributesService requirementsAttributes,
            IUnitOfWork unit) : base(repository, unit)
        {
            this.requirementsAttributes = requirementsAttributes;
        }

        public override void add(ref LinkBetweenRequirementsEntity entity, bool persistir = true)
        {
            try
            {
                if (!entity.RequirementOriginID.Equals(entity.RequirementTargetID))
                    base.add(ref entity, persistir);
                else
                    throw new ArgumentException("Requirement Origin must be different from Requirement Target!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LinkBetweenRequirementsEntity get(string ReqOrigin, string ReqTarget)
        {
            try
            {
                return convertModelToEntity(repository.filter(l => l.RequirementOrigin.code == ReqOrigin && l.RequirementTarget.code == ReqTarget).SingleOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LinkBetweenRequirementsEntity getWithCode(string code)
        {
            try
            {
                return convertModelToEntity(repository.filter(l => l.code == code).SingleOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
