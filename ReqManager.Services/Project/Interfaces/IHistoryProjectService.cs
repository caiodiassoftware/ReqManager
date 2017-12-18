using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Project.Interfaces
{
    public interface IHistoryProjectService : IService<HistoryProjectEntity>
    {
        IEnumerable<HistoryProjectEntity> getProjectHistory(int ProjectID);
    }
}
