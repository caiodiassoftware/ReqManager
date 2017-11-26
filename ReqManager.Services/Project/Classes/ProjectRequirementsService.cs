using System;
using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace ReqManager.Services.Project.Classes
{
    public class ProjectRequirementsService : 
        ServiceBase<ProjectRequirements, ProjectRequirementsEntity>, IProjectRequirementsService
    {
        public ProjectRequirementsService(IProjectRequirementsRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
        }

        public IEnumerable<ProjectRequirementsEntity> getRequirementsByProject(int ProjectID)
        {
            try
            {
                return getAll().Where(r => r.ProjectID.Equals(ProjectID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProjectRequirementsEntity getRequirementsByProjectAndRequirement(int ProjectID, int RequirementID)
        {
            try
            {
                return getRequirementsByProject(ProjectID).Where(r => r.RequirementID.Equals(RequirementID)).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool isTraceable(int ProjectID, int RequirementID)
        {
            try
            {
                return getAll().Where(r => r.ProjectID.Equals(ProjectID) && r.RequirementID.Equals(RequirementID)).SingleOrDefault().traceable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
