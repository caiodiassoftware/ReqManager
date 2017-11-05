using System;
using System.Collections.Generic;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Link.Interfaces;
using ReqManager.Entities.Link;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Link.Interfaces;

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

        public void add(LinkBetweenRequirementsEntity entity, List<LinkRequirementAttributesEntity> attributes)
        {
            try
            {
                unit.BeginTransaction();

                add(ref entity);
                attributes.ForEach(a => {
                    a.LinkRequirementsID = entity.LinkRequirementsID;
                    requirementsAttributes.add(ref a);
                });

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
