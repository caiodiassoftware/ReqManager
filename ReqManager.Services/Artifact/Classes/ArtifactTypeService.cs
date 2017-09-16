using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Acess.Interfaces;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Services.Project.Classes
{
    public class ArtifactTypeService : ServiceBase<ArtifactType>, IArtifactTypeService
    {
        public ArtifactTypeService(IArtifactTypeRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
