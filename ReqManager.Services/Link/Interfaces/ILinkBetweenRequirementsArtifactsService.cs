using ReqManager.Entities.Link;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Link.Interfaces
{
    public interface ILinkBetweenRequirementsArtifactsService : IService<LinkBetweenRequirementsArtifactsEntity>
    {
        LinkBetweenRequirementsArtifactsEntity getWithCode(string code);
    }
}
