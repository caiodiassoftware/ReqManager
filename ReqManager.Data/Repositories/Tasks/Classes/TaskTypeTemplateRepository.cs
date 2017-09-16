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
    
    public class TaskTypeTemplateRepository : RepositoryBase<TaskTypeTemplate>, ITaskTypeTemplateRepository
    {
        public TaskTypeTemplateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
