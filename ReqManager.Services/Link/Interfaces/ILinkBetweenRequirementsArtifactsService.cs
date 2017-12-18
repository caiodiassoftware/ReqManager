using ReqManager.Entities.Link;
using ReqManager.Services.Estructure;

namespace ReqManager.Services.Link.Interfaces
{
    public interface ILinkBetweenRequirementsArtifactsService : IService<LinkBetweenRequirementsArtifactsEntity>
    {
        LinkBetweenRequirementsArtifactsEntity getWithCode(string code);
        LinkBetweenRequirementsArtifactsEntity get(string ArtifactCode, string RequirementCode);
    }
}
