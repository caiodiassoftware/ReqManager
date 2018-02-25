using ReqManager.Services.Estructure;
using ReqManager.Entities;
using System.Collections.Generic;

namespace ReqManager.Services.InterfacesServices
{
    public interface IControllerActionService : IService<Entities.ControllerActionEntity>
    {
        List<ControllerActionEntity> GetPermissions(int UserID);
        void Refresh(IEnumerable<ControllerActionEntity> controllerActionApplication);
        bool CanAccess(List<ControllerActionEntity> controllerActions, string controllerName, string actionName);
    }
}
