using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class StakeholderRequirementEntity
    {
        [Key]
        [Display(Name = "StakeHolder Requirement")]
        public int StakeHolderRequirementID { get; set; }
        [Required]
        [Display(Name = "Project Requirements")]
        public int ProjectRequirementID { get; set; }
        [Required]
        [Display(Name = "Stakeholders")]
        public int StakeHolderID { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; } = DateTime.Now;

        public virtual ProjectRequirementsEntity ProjectRequirements { get; set; }
        public virtual StakeholdersEntity StakeHolders { get; set; }

        public String DisplayName
        {
            get
            {
                return this.StakeHolders.Users.nickName + " - " + this.ProjectRequirements.Project.description;
            }
        }

    }
}
