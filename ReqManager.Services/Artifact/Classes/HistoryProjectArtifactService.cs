using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Artifact.Interfaces;
using ReqManager.Entities.Artifact;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;

namespace ReqManager.Services.Project.Classes
{
    public class HistoryProjectArtifactService : ServiceBase<HistoryProjectArtifact, HistoryProjectArtifactEntity>, IHistoryProjectArtifactService
    {
        public HistoryProjectArtifactService(IHistoryProjectArtifactRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
