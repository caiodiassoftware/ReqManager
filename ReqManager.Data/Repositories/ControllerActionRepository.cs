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
    public class ControllerActionRepository : RepositoryBase<ControllerAction>, IControllerActionRepository
    {
        public ControllerActionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
