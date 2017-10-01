using ReqManager.Entities.Artifact;
using ReqManager.ManagerController;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Controllers
{
    public class ArtifactTypeController : BaseController<ArtifactTypeEntity>
    {
        public ArtifactTypeController(IArtifactTypeService service) : base(service)
        {
        }
    }
}
