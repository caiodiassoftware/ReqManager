﻿using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Services.Project.Classes
{
    public class HistoryProjectService : ServiceBase<HistoryProject, HistoryProjectEntity>, IHistoryProjectService
    {
        public HistoryProjectService(IHistoryProjectRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
