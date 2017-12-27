using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Artifact;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReqManager.Services.Project.Classes
{
    public class ProjectArtifactService : ServiceBase<ProjectArtifact, ProjectArtifactEntity>, 
        IProjectArtifactService
    {
        private IHistoryProjectArtifactService historyServiceArtifact { get; set; }

        public ProjectArtifactService(
            IProjectArtifactRepository repository,
            IHistoryProjectArtifactService historyServiceArtifact,
            IUnitOfWork unit) : base(repository, unit)
        {
            this.historyServiceArtifact = historyServiceArtifact;
        }

        public IEnumerable<ProjectArtifactEntity> getArtifactsByProject(int ProjectID)
        {
            try
            {
                return getAll().Where(p => p.ProjectID.Equals(ProjectID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProjectArtifactEntity GetWithCode(string code)
        {
            try
            {
                return getAll().Where(p => p.code.Equals(code)).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void update(ProjectArtifactEntity entity, string login)
        {

            try
            {
                unit.BeginTransaction();
                HistoryProjectArtifactEntity history = new HistoryProjectArtifactEntity();
                ProjectArtifactEntity artifact = get(entity.ProjectArtifactID);
                history.ProjectArtifactID = artifact.ProjectArtifactID;
                history.description = artifact.description;
                history.descriptionImportance = artifact.Importance.description;
                history.descriptionTypeArtifact = artifact.ArtifactType.description;
                history.login = login;
                history.path = artifact.path;

                base.update(ref entity, false);
                historyServiceArtifact.add(ref history, false);
                unit.Commit();
            }
            catch (Exception ex)
            {
                unit.Rollback();
                throw ex;
            }

        }
    }
}
