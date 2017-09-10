using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("USERS", Schema = "ACESS")]
    public class USERS
    {
        public USERS()
        {
            this.UserRole = new HashSet<USER_ROLE>();
            this.StakeHolders = new HashSet<STAKEHOLDERS>();
            this.ProjectArtifact = new HashSet<PROJECT_ARTIFACT>();
            this.LinkRequirementsArtifacts = new HashSet<LINK_BETWEEN_REQUIREMENTS_ARTIFACTS>();
            this.LinkRequirements = new HashSet<LINK_BETWEEN_REQUIREMENT>();
            this.HistoryProject = new HashSet<HISTORY_PROJECT>();
            this.StakeHolderRequirement = new HashSet<STAKEHOLDER_REQUIREMENT>();
            this.Project = new HashSet<PROJECT>();
            this.StakeHolderProject = new HashSet<STAKEHOLDERS_PROJECT>();
            this.ProjectRequirement = new HashSet<PROJECT_REQUIREMENTS>();
            this.HistoryTask = new HashSet<HISTORY_TASK>();
            this.TaskTypeTemplate = new HashSet<TASK_TYPE_TEMPLATE>();
            this.Requirement = new HashSet<REQUIREMENT>();
            this.RequirementTemplate = new HashSet<REQUIREMENT_TEMPLATE>();
            this.UserTask = new HashSet<USER_TASK>();
            this.Task = new HashSet<TASK>();
            this.TypeLink = new HashSet<TYPE_LINK>();
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
    
        public virtual ICollection<USER_ROLE> UserRole { get; set; }
        public virtual ICollection<STAKEHOLDERS> StakeHolders { get; set; }
        public virtual ICollection<PROJECT_ARTIFACT> ProjectArtifact { get; set; }
        public virtual ICollection<LINK_BETWEEN_REQUIREMENTS_ARTIFACTS> LinkRequirementsArtifacts { get; set; }
        public virtual ICollection<LINK_BETWEEN_REQUIREMENT> LinkRequirements { get; set; }
        public virtual ICollection<HISTORY_PROJECT> HistoryProject { get; set; }
        public virtual ICollection<STAKEHOLDER_REQUIREMENT> StakeHolderRequirement { get; set; }
        public virtual ICollection<PROJECT> Project { get; set; }
        public virtual ICollection<STAKEHOLDERS_PROJECT> StakeHolderProject { get; set; }
        public virtual ICollection<PROJECT_REQUIREMENTS> ProjectRequirement { get; set; }
        public virtual ICollection<HISTORY_TASK> HistoryTask { get; set; }
        public virtual ICollection<TASK_TYPE_TEMPLATE> TaskTypeTemplate { get; set; }
        public virtual ICollection<REQUIREMENT> Requirement { get; set; }
        public virtual ICollection<REQUIREMENT_TEMPLATE> RequirementTemplate { get; set; }
        public virtual ICollection<USER_TASK> UserTask { get; set; }
        public virtual ICollection<TASK> Task { get; set; }
        public virtual ICollection<TYPE_LINK> TypeLink { get; set; }
    }
}
