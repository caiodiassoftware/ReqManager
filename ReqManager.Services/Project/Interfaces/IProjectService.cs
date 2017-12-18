using ReqManager.Entities.Project;
using ReqManager.Services.Estructure;

namespace ReqManager.Services.Project.Interfaces
{
    public interface IProjectService : IService<ProjectEntity>
    {
        bool isPreTraceability(ProjectEntity project);
        bool isPosTraceability(ProjectEntity project);
        void update(ref ProjectEntity entity, int UserID);
    }
}
