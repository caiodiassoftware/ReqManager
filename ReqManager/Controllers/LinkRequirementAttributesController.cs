using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;

namespace ReqManager.Controllers
{
    public class LinkRequirementAttributesController : BaseController<LinkRequirementAttributesEntity>
    {
        public LinkRequirementAttributesController(
            IService<LinkRequirementAttributesEntity> service,
            IService<AttributesEntity> attributeService,
            IService<LinkBetweenRequirementsEntity> linkRequirementsService) : base(service)
        {
            ViewData.Add("AttributeID", new SelectList(attributeService.getAll(), "AttributeID", "description"));
            ViewData.Add("LinkRequirementsID", new SelectList(linkRequirementsService.getAll(), "LinkRequirementsID", "code"));
        }
    }
}
