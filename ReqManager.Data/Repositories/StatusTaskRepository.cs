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
    public class StatusTaskRepository : RepositoryBase<STATUS_TASK>, IStatusTaskRepository
    {
        public StatusTaskRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
