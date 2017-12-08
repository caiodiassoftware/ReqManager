using ReqManager.Services.Estructure;
using ReqManager.Entities;
using System.Collections.Generic;

namespace ReqManager.Services.InterfacesServices
{
    public interface IControllerActionService : IService<Entities.ControllerActionEntity>
    {
        void Refresh(IEnumerable<ControllerActionEntity> controllerActionApplication);
        bool CanAccess(int UserID, string controllerName, string actionName);
    }
}
