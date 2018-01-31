using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;
using System.Linq;

namespace ReqManager.Data.Repositories
{
    public class UserRepository : RepositoryBase<Users>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Users GetUserByLogin(string login)
        {
            return DbContext.user.Where(c => c.login == login).FirstOrDefault();
        }
    }
}
