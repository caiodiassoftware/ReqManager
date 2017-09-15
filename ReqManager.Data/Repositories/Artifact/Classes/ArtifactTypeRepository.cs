using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Acess.Interfaces;
using ReqManager.Model;

namespace ReqManager.Data.Repositories.Acess.Classes
{
    public class ArtifactTypeRepository : RepositoryBase<ArtifactType>, IArtifactTypeRepository
    {
        public ArtifactTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
