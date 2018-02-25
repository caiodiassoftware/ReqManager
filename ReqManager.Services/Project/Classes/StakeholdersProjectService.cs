using ReqManager.Data.Infrastructure;
using ReqManager.Data.Repositories.Project.Interfaces;
using ReqManager.Entities.Project;
using ReqManager.Model;
using ReqManager.Services.Estructure;
using ReqManager.Services.Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReqManager.Services.Project.Classes
{
    public class StakeholdersProjectService : ServiceBase<StakeholdersProject, StakeholdersProjectEntity>, 
        IStakeholdersProjectService
    {
        private IStakeholdersProjectRepository repository { get; set; }

        public StakeholdersProjectService(IStakeholdersProjectRepository repository, IUnitOfWork unit) : base(repository, unit)
        {
            this.repository = repository;
        }

        public StakeholdersProjectEntity getByProjectAndUser(int ProjectID, int UserID)
        {
            try
            {
                return convertModelToEntity(repository.filter(p => p.ProjectID.Equals(ProjectID) &&
                p.Stakeholders.UserID.Equals(UserID)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<StakeholdersProjectEntity> getStakeholderByProject(int ProjectID)
        {
            try
            {
                return convertEnumerableModelToEntity(repository.getStakeholderByProject(ProjectID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
