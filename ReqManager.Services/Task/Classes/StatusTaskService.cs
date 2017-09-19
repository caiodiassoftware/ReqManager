using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Entities.Task;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Task.Interfaces;

namespace ReqManager.Services.Task.Classes
{
    public class StatusTaskService : ServiceBase<StatusTask, StatusTaskEntity>, IStatusTaskService
    {
        public StatusTaskService(IStatusTaskRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
