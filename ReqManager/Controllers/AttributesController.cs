using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Link.Interfaces;

namespace ReqManager.Controllers
{
    public class AttributesController : BaseController<AttributesEntity>
    {
        public AttributesController(IAttributesService service) : base(service) {}
    }
}
