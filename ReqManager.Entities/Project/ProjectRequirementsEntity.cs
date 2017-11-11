using ReqManager.Entities.Acess;
using ReqManager.Entities.Requirement;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class ProjectRequirementsEntity
    {
        [Key]
        [Display(Name = "Project Requirement")]
        public int ProjectRequirementID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Required]
        [Display(Name = "Requirement")]
        public int RequirementID { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; } = DateTime.Now;
        [Required]
        [Display(Name = "Traceable")]
        public bool traceable { get; set; }

        public String DisplayName
        {
            get
            {
                return this.Project.description + " - " + Requirement.code;
            }
        }

        public virtual UserEntity Users { get; set; }
        public virtual ProjectEntity Project { get; set; }
        public virtual RequirementEntity Requirement { get; set; }
    }
}
