using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Artifact.Interfaces;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Artifact;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Project.Classes
{
    public class ProjectArtifactService : ServiceBase<ProjectArtifact, ProjectArtifactEntity>, IProjectArtifactService
    {
        public ProjectArtifactService(IProjectArtifactRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }
    }
}
