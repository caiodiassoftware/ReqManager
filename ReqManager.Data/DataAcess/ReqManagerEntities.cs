using ReqManager.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.DataAcess
{
    public class ReqManagerEntities : DbContext
    {
        public ReqManagerEntities() : base("ReqManagerDataEntities")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<ControllerAction> controllerAction { get; set; }
        public virtual DbSet<Role> role { get; set; }
        public virtual DbSet<RoleControllerAction> roleControllerAction { get; set; }
        public virtual DbSet<UserRole> userRole { get; set; }
        public virtual DbSet<Users> user { get; set; }
        public virtual DbSet<ArtifactType> artifactType { get; set; }
        public virtual DbSet<HistoryProjectArtifact> historyProjectArtifact { get; set; }
        public virtual DbSet<ProjectArtifact> ProjectArtifact { get; set; }
        public virtual DbSet<Attributes> Attributes { get; set; }
        public virtual DbSet<AttributesTypeLink> AttributesTypeLink { get; set; }
        public virtual DbSet<LinkArtifactAttributes> LinkArtifactAttributes { get; set; }
        public virtual DbSet<LinkBetweenRequirement> LinkBetweenRequirement { get; set; }
        public virtual DbSet<LinkBetweenRequirementsArtifacts> LinkBetweenRequirementsArtifacts { get; set; }
        public virtual DbSet<LinkRequirementAttributes> LinkRequirementAttributes { get; set; }
        public virtual DbSet<TypeLink> TypeLink { get; set; }
        public virtual DbSet<HistoryProject> HistoryProject { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectPhases> ProjectPhases { get; set; }
        public virtual DbSet<ProjectRequirements> ProjectRequirements { get; set; }
        public virtual DbSet<StakeholderRequirement> StakeholderRequirement { get; set; }
        public virtual DbSet<Stakeholders> Stakeholders { get; set; }
        public virtual DbSet<StakeholdersProject> StakeholdersProject { get; set; }
        public virtual DbSet<MeasureImportance> MeasureImportance { get; set; }
        public virtual DbSet<Requirement> Requirement { get; set; }
        public virtual DbSet<RequirementActionHistory> RequirementActionHistory { get; set; }
        public virtual DbSet<RequirementRationale> RequirementRationale { get; set; }
        public virtual DbSet<RequirementStatus> RequirementStatus { get; set; }
        public virtual DbSet<RequirementTemplate> RequirementTemplate { get; set; }
        public virtual DbSet<RequirementType> RequirementTask { get; set; }
        public virtual DbSet<StakeholderClassification> StakeholderClassification { get; set; }
        public virtual DbSet<HistoryTask> HistoryTask { get; set; }
        public virtual DbSet<StatusTask> StatusTask { get; set; }
        public virtual DbSet<Model.Task> Task { get; set; }
        public virtual DbSet<Subtask> Subtask { get; set; }
        public virtual DbSet<TaskType> TaskType { get; set; }
        public virtual DbSet<TaskTypeTemplate> TaskTypeTemplate { get; set; }
        public virtual DbSet<UserTask> UserTask { get; set; }
    }
}
