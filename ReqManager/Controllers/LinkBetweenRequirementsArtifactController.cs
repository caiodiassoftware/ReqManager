using System.Web.Mvc;
using ReqManager.Entities.Link;
using ReqManager.ManagerController;
using ReqManager.Services.Estructure;
using ReqManager.Entities.Artifact;
using ReqManager.Entities.Requirement;
using ReqManager.Entities.Acess;

namespace ReqManager.Controllers
{
    public class LinkBetweenRequirementsArtifactController : BaseController<LinkBetweenRequirementsArtifactsEntity>
    {
        public LinkBetweenRequirementsArtifactController(
            IService<LinkBetweenRequirementsArtifactsEntity> service,
            IService<ProjectArtifactEntity> artifactService,
            IService<RequirementEntity> reqService,
            IService<UserEntity> userService,
            IService<TypeLinkEntity> typeService) : base(service)
        {
            ViewData.Add("ProjectArtifactID", new SelectList(artifactService.getAll(), "ProjectArtifactID", "code"));
            ViewData.Add("RequirementID", new SelectList(reqService.getAll(), "RequirementID", "code"));
            ViewData.Add("TypeLinkID", new SelectList(typeService.getAll(), "TypeLinkID", "description"));
            ViewData.Add("UserID", new SelectList(userService.getAll(), "UserID", "name"));
        }

    }
}
