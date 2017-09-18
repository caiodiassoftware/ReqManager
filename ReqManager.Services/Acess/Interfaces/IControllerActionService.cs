using ReqManager.Services.Estructure;
using System.Collections;
using ReqManager.Model;

namespace ReqManager.Services.InterfacesServices
{
    public interface IControllerActionService : IService<Entities.ControllerActionEntity>
    {
        void Refresh(IEnumerable controllerActionApplication);
    }
}
