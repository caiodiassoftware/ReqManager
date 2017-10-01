using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;

namespace ReqManager.Controllers
{
    public class LinkArtifactAttributesController : BaseController<LinkArtifactAttributesEntity>
    {
        public LinkArtifactAttributesController(
            IService<LinkArtifactAttributesEntity> service,
            IService<AttributesEntity> attributeService,
            IService<LinkBetweenRequirementsArtifactsEntity> reqArtService) : base(service)
        {
            ViewData.Add("AttributeID", new SelectList(attributeService.getAll(), "AttributeID", "description"));
            ViewData.Add("LinkArtifactRequirementID", new SelectList(reqArtService.getAll(), "LinkArtifactRequirementID", "DisplayName"));
        }
    }
}
