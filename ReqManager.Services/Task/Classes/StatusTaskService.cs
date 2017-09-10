using ReqManager.Data.Infrastructure;
using ReqManager.Data.InterfacesRepositories;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Task.Interfaces;

namespace ReqManager.Services.Task.Classes
{
    public class StatusTaskService : ServiceBase<STATUS_TASK>, IStatusTaskService
    {
        public StatusTaskService(IStatusTaskRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
