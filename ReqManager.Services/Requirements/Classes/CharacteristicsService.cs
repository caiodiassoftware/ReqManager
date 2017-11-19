using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Requirements.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Requirements.Classes
{
    public class CharacteristicsService : ServiceBase<Characteristics, CharacteristicsEntity>, ICharacteristicsService
    {
        public CharacteristicsService(ICharacteristicsRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
