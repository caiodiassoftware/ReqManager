using ReqManager.Entities.Link;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Link.Interfaces
{
    public interface ILinkBetweenRequirementsService : IService<LinkBetweenRequirementsEntity>
    {
        void add(LinkBetweenRequirementsEntity entity, List<LinkRequirementAttributesEntity> attributes);
    }
}
