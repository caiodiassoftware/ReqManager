using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using System;

namespace ReqManager.Services.Project.Classes
{
    public class ProjectService : ServiceBase<Model.Project, ProjectEntity>, IProjectService
    {
        public ProjectService(IProjectRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }

        public bool isPreTraceability(ProjectEntity project)
        {
            try
            {
                return project.ProjectPhases.ProjectPhasesID.Equals(1) || project.ProjectPhases.ProjectPhasesID.Equals(2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool isPosTraceability(ProjectEntity project)
        {
            try
            {
                return !isPreTraceability(project);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
