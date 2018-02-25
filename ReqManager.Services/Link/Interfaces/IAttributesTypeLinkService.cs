using ReqManager.Entities.Link;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Link.Interfaces
{
    public interface IAttributesTypeLinkService : IService<AttributesTypeLinkEntity>
    {
        IEnumerable<AttributesTypeLinkEntity> GetByTypeLink(int TypeLinkID);
    }
}
