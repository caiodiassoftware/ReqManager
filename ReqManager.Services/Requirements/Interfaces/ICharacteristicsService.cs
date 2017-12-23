using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Requirements.Interfaces
{
    public interface ICharacteristicsService : IService<CharacteristicsEntity>
    {
        IEnumerable<CharacteristicsEntity> getRequiredCharacteristics();
    }
}
