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
                    throw new ArgumentException("Requirement Origin must be different from B!");
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
                return getAll().Where(l => l.RequirementOrigin.code.Equals(ReqOrigin) && l.RequirementTarget.code.Equals(ReqTarget)).SingleOrDefault();
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
                return getAll().Where(l => l.code.Equals(code)).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
