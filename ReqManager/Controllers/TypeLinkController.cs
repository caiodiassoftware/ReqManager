using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Link.Interfaces;
using ReqManager.Services.Acess.Interfaces;

namespace ReqManager.Controllers
{
    public class TypeLinkController : BaseController<TypeLinkEntity>
    {
        public TypeLinkController(ITypeLinkService service, IUserService userService) : base(service)
        {
            ViewBag.CreationUserID = new SelectList(userService.getAll(), "UserID", "name");
        }        
    }
}
