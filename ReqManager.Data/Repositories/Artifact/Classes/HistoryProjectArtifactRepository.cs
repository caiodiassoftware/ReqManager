using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Artifact.Interfaces;
using ReqManager.Model;

namespace ReqManager.Data.Repositories.Artifact.Classes
{
    public class HistoryProjectArtifactRepository : RepositoryBase<HistoryProjectArtifact>, IHistoryProjectArtifactRepository
    {
        public HistoryProjectArtifactRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
