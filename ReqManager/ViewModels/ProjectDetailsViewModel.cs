using ReqManager.Entities.Artifact;
using ReqManager.Entities.Project;
using ReqManager.Entities.Requirement;
using System.Collections.Generic;

namespace ReqManager.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public ProjectEntity project { get; set; }
        public IEnumerable<RequirementEntity> requirements { get; set; }
        public IEnumerable<ProjectArtifactEntity> artifacts { get; set; }

        public ProjectDetailsViewModel()
        {
            requirements = new List<RequirementEntity>();
            artifacts = new List<ProjectArtifactEntity>();
        }
    }
}