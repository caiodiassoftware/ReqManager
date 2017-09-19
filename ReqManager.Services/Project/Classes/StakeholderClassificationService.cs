using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Acess;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Project.Classes
{

    public class StakeholderClassificationService : ServiceBase<StakeholderClassification, StakeholderClassificationEntity>, IStakeholderClassificationService
    {
        public StakeholderClassificationService(IStakeholderClassificationRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
