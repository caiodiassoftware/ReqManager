using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Link.Interfaces;
using ReqManager.Model;

namespace ReqManager.Data.Repositories.Link.Classes
{


    public class LinkArtifactAttributesRepository : RepositoryBase<LinkArtifactAttributes>, ILinkArtifactAttributesRepository
    {
        public LinkArtifactAttributesRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
