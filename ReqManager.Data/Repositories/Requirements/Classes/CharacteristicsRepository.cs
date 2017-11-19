using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Model;

namespace ReqManager.Data.Repositories.Requirements.Classes
{
    public class CharacteristicsRepository : RepositoryBase<Characteristics>, ICharacteristicsRepository
    {
        public CharacteristicsRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

}
