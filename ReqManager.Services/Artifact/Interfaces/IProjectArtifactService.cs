using ReqManager.Entities.Artifact;
using ReqManager.Services.Estructure;
using System.Collections.Generic;

namespace ReqManager.Services.Project.Interfaces
{
    public interface IProjectArtifactService : IService<ProjectArtifactEntity>
    {
        IEnumerable<ProjectArtifactEntity> getArtifactsByProject(int ProjectID);
        ProjectArtifactEntity GetWithCode(string code);
    }
}
