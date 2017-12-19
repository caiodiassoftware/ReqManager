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
        public int StakeholdersProjectID { get; set; }
        [Range(1, Double.PositiveInfinity)]
        public int RequirementID { get; set; }
        public DateTime creationDate { get; set; } = DateTime.Now;

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
