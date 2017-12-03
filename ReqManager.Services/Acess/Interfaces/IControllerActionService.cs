using ReqManager.Services.Estructure;
using ReqManager.Entities;
using System.Collections.Generic;
using ReqManager.Entities.Acess;

namespace ReqManager.Services.InterfacesServices
{
    public interface IControllerActionService : IService<Entities.ControllerActionEntity>
    {
        void Refresh(IEnumerable<ControllerActionEntity> controllerActionApplication);
        IEnumerable<ControllerActionEntity> GetActionsFromUser(UserEntity user);
    }
}
