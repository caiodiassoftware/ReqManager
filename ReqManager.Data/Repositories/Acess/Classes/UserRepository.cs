using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Repositories
{
    public class UserRepository : RepositoryBase<Users>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Users GetUserByLogin(string login)
        {
            return (Users)this.DbContext.user.Where(c => c.login == login).FirstOrDefault();
        }
    }
}
