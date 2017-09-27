using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Tasks.Interfaces;
using ReqManager.Entities.Acess;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Task.Interfaces;

namespace ReqManager.Services.Task.Classes
{
    public class HistoryTaskService : ServiceBase<HistoryTask, HistoryTaskEntity>, IHistoryTaskService
    {
        public HistoryTaskService(IHistoryTaskRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
