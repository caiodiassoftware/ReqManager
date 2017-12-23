using ReqManager.Entities.Requirement;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class StakeholderRequirementEntity
    {
        [Key]
        public int StakeholderRequirementID { get; set; }
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Project Stakeholder")]
        public int StakeholdersProjectID { get; set; }
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Requirement")]
        public int RequirementID { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; } = DateTime.Now;
        [Required]
        [Range(0, 9)]
        [Display(Name = "Importance to Requirement")]
        public int importanceValue { get; set; }

        [Display(Name = "Stakeholder")]
        public String DisplayName
        {
            get
            {
                return this.StakeholdersProject.DisplayName + " : " + this.Requirement.DisplayName;
            }
        }

        public virtual StakeholdersProjectEntity StakeholdersProject { get; set; }
        public virtual RequirementEntity Requirement { get; set; }
    }
}
