
using ReqManager.Model;
using ReqManager.Services.Estructure;
using System.Collections;

namespace ReqManager.Services.InterfacesServices
{
    public interface IControllerActionService : IService<ControllerAction>
    {
        void Refresh(IEnumerable controllerActionApplication);
    }
}
