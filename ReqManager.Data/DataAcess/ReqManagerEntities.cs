using ReqManager.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.DataAcess
{
    public class ReqManagerEntities : DbContext
    {
        public ReqManagerEntities() : base("ReqManagerDataEntities")
        {

        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<ControllerAction> CONTROLLER_ACTION { get; set; }
        public virtual DbSet<Role> ROLE { get; set; }
        public virtual DbSet<RoleControllerAction> ROLE_CONTROLLER_ACTION { get; set; }
        public virtual DbSet<UserRole> USER_ROLE { get; set; }
        public virtual DbSet<Users> USERS { get; set; }
        public virtual DbSet<ArtifactType> ARTIFACT_TYPE { get; set; }
        public virtual DbSet<HistoryProjectArtifact> HISTORY_PROJECT_ARTIFACT { get; set; }
        public virtual DbSet<ProjectArtifact> PROJECT_ARTIFACT { get; set; }
        public virtual DbSet<Attributes> ATTRIBUTES { get; set; }
        public virtual DbSet<AttributesTypeLink> ATTRIBUTES_TYPE_LINK { get; set; }
        public virtual DbSet<LinkArtifactAttributes> LINK_ARTIFACT_ATTRIBUTES { get; set; }
        public virtual DbSet<LinkBetweenRequirement> LINK_BETWEEN_REQUIREMENT { get; set; }
        public virtual DbSet<LinkBetweenRequirementsArtifacts> LINK_BETWEEN_REQUIREMENTS_ARTIFACTS { get; set; }
        public virtual DbSet<LinkRequirementAttributes> LINK_REQUIREMENT_ATTRIBUTES { get; set; }
        public virtual DbSet<TypeLink> TYPE_LINK { get; set; }
        public virtual DbSet<HistoryProject> HISTORY_PROJECT { get; set; }
        public virtual DbSet<Project> PROJECT { get; set; }
        public virtual DbSet<ProjectPhases> PROJECT_PHASES { get; set; }
        public virtual DbSet<ProjectRequirements> PROJECT_REQUIREMENTS { get; set; }
        public virtual DbSet<StakeholderRequirement> STAKEHOLDER_REQUIREMENT { get; set; }
        public virtual DbSet<Stakeholders> STAKEHOLDERS { get; set; }
        public virtual DbSet<StakeholdersProject> STAKEHOLDERS_PROJECT { get; set; }
        public virtual DbSet<MeasureImportance> MEASURE_IMPORTANCE { get; set; }
        public virtual DbSet<Requirement> REQUIREMENT { get; set; }
        public virtual DbSet<RequirementActionHistory> REQUIREMENT_ACTION_HISTORY { get; set; }
        public virtual DbSet<RequirementRationale> REQUIREMENT_RATIONALE { get; set; }
        public virtual DbSet<RequirementStatus> REQUIREMENT_STATUS { get; set; }
        public virtual DbSet<RequirementTemplate> REQUIREMENT_TEMPLATE { get; set; }
        public virtual DbSet<RequirementTask> REQUIREMENT_TYPE { get; set; }
        public virtual DbSet<StakeholderClassification> STAKEHOLDER_CLASSIFICATION { get; set; }
        public virtual DbSet<HistoryTask> HISTORY_TASK { get; set; }
        public virtual DbSet<StatusTask> STATUS_TASK { get; set; }
        public virtual DbSet<Model.Task> TASK { get; set; }
        public virtual DbSet<TaskType> TASK_TYPE { get; set; }
        public virtual DbSet<TaskTypeTemplate> TASK_TYPE_TEMPLATE { get; set; }
        public virtual DbSet<UserTask> USER_TASK { get; set; }
    }
}
