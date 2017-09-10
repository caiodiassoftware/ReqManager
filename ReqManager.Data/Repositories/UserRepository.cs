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
    public class UserRepository : RepositoryBase<USERS>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public USERS GetUserByLogin(string login)
        {
            return (USERS)this.DbContext.USERS.Where(c => c.login == login).FirstOrDefault();
        }
    }
}
