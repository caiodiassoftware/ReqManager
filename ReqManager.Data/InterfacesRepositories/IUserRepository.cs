using ReqManager.Data.Infrastructure;
using ReqManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.InterfacesRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByLogin(string login);
    }
}
