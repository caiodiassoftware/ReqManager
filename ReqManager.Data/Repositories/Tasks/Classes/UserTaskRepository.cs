using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Tasks.Interfaces;
using ReqManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Repositories.Tasks.Classes
{
    public class UserTaskRepository : RepositoryBase<UserTask>, IUserTaskRepository
    {
        public UserTaskRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
