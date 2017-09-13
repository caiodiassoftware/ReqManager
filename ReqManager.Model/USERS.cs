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
            this.UserRole = new HashSet<UserRole>();
            this.StakeHolders = new HashSet<Stakeholders>();
            this.ProjectArtifact = new HashSet<ProjectArtifact>();
            this.LinkRequirementsArtifacts = new HashSet<LinkBetweenRequirementsArtifacts>();
            this.LinkRequirements = new HashSet<LinkBetweenRequirement>();
            this.HistoryProject = new HashSet<HistoryProject>();
            this.Project = new HashSet<Project>();
            this.ProjectRequirement = new HashSet<ProjectRequirements>();
            this.HistoryTask = new HashSet<HistoryTask>();
            this.TaskTypeTemplate = new HashSet<TaskTypeTemplate>();
            this.Requirement = new HashSet<Requirement>();
            this.RequirementTemplate = new HashSet<RequirementTemplate>();
            this.UserTask = new HashSet<UserTask>();
            this.Task = new HashSet<Task>();
            this.TypeLink = new HashSet<TypeLink>();
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

        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<Stakeholders> StakeHolders { get; set; }
        public virtual ICollection<ProjectArtifact> ProjectArtifact { get; set; }
        public virtual ICollection<LinkBetweenRequirementsArtifacts> LinkRequirementsArtifacts { get; set; }
        public virtual ICollection<LinkBetweenRequirement> LinkRequirements { get; set; }
        public virtual ICollection<HistoryProject> HistoryProject { get; set; }
        public virtual ICollection<Project> Project { get; set; }
        public virtual ICollection<ProjectRequirements> ProjectRequirement { get; set; }
        public virtual ICollection<HistoryTask> HistoryTask { get; set; }
        public virtual ICollection<TaskTypeTemplate> TaskTypeTemplate { get; set; }
        public virtual ICollection<Requirement> Requirement { get; set; }
        public virtual ICollection<RequirementTemplate> RequirementTemplate { get; set; }
        public virtual ICollection<UserTask> UserTask { get; set; }
        public virtual ICollection<Task> Task { get; set; }
        public virtual ICollection<TypeLink> TypeLink { get; set; }
    }
}
