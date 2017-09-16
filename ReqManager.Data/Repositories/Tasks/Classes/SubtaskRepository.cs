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
    public class SubtaskRepository : RepositoryBase<Subtask>, ISubtaskRepository
    {
        public SubtaskRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
