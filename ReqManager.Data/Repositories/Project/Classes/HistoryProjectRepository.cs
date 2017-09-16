using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Link.Interfaces;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Repositories.Project.Classes
{
    public class HistoryProjectRepository : RepositoryBase<HistoryProject>, IHistoryProjectRepository
    {
        public HistoryProjectRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
