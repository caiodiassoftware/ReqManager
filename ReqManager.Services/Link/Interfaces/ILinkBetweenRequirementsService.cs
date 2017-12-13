using ReqManager.Entities.Link;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Link.Interfaces
{
    public interface ILinkBetweenRequirementsService : IService<LinkBetweenRequirementsEntity>
    {
        LinkBetweenRequirementsEntity get(string ReqOrigin, string ReqTarget);
        LinkBetweenRequirementsEntity getWithCode(string code);
    }
}
