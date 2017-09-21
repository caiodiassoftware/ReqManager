using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("USERS", Schema = "ACESS")]
    public class Users
    {
        public Users()
        {
            this.UserRoleModel = new HashSet<UserRole>();
            this.StakeHoldersModel = new HashSet<Stakeholders>();
            this.ProjectArtifactModel = new HashSet<ProjectArtifact>();
            this.LinkRequirementsArtifactsModel = new HashSet<LinkBetweenRequirementsArtifacts>();
            this.LinkRequirementsModel = new HashSet<LinkBetweenRequirement>();
            this.HistoryProjectModel = new HashSet<HistoryProject>();
            this.ProjectModel = new HashSet<Project>();
            this.ProjectRequirementModel = new HashSet<ProjectRequirements>();
            this.HistoryTaskModel = new HashSet<HistoryTask>();
            this.TaskTypeTemplateModel = new HashSet<TaskTypeTemplate>();
            this.RequirementModel = new HashSet<Requirement>();
            this.RequirementTemplateModel = new HashSet<RequirementTemplate>();
            this.UserTaskModel = new HashSet<UserTask>();
            this.TaskModel = new HashSet<Task>();
            this.TypeLinkModel = new HashSet<TypeLink>();
        }

        [Key]
        public int UserID { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string nickName { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(1000)]
        public string password { get; set; }
        [Required]
        [MaxLength(50)]
        public string email { get; set; }
        [Required]
        [MaxLength(15)]
        [MinLength(6)]
        public string login { get; set; }
        [Required]
        public System.DateTime dateOfBirth { get; set; }
        [Required]
        [MaxLength(30)]
        public string profession { get; set; }
        [Required]
        [MaxLength(20)]
        public string document { get; set; }
        [Required]
        public Boolean active { get; set; }

        public virtual ICollection<UserRole> UserRoleModel { get; set; }
        public virtual ICollection<Stakeholders> StakeHoldersModel { get; set; }
        public virtual ICollection<ProjectArtifact> ProjectArtifactModel { get; set; }
        public virtual ICollection<LinkBetweenRequirementsArtifacts> LinkRequirementsArtifactsModel { get; set; }
        public virtual ICollection<LinkBetweenRequirement> LinkRequirementsModel { get; set; }
        public virtual ICollection<HistoryProject> HistoryProjectModel { get; set; }
        public virtual ICollection<Project> ProjectModel { get; set; }
        public virtual ICollection<ProjectRequirements> ProjectRequirementModel { get; set; }
        public virtual ICollection<HistoryTask> HistoryTaskModel { get; set; }
        public virtual ICollection<TaskTypeTemplate> TaskTypeTemplateModel { get; set; }
        public virtual ICollection<Requirement> RequirementModel { get; set; }
        public virtual ICollection<RequirementTemplate> RequirementTemplateModel { get; set; }
        public virtual ICollection<UserTask> UserTaskModel { get; set; }
        public virtual ICollection<Task> TaskModel { get; set; }
        public virtual ICollection<TypeLink> TypeLinkModel { get; set; }
    }
}
