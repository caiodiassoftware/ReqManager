using ReqManager.Services.Estructure;
using System.Collections;
using ReqManager.Model;
using ReqManager.Entities;
using System.Collections.Generic;

namespace ReqManager.Services.InterfacesServices
{
    public interface IControllerActionService : IService<Entities.ControllerActionEntity>
    {
        void Refresh(IEnumerable<ControllerActionEntity> controllerActionApplication);
    }
}
