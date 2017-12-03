using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class StakeholderRequirementApprovalEntity
    {
        [Key]
        [Display(Name = "StakeHolder Requirement")]
        public int StakeHolderRequirementApprovalID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Project Requirements")]
        public int ProjectRequirementID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Stakeholders")]
        public int StakeholdersProjectID { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; } = DateTime.Now;
        [MinLength(6)]
        [MaxLength(255)]
        [DefaultValue("NOT RATED")]
        public string description { get; set; }
        public bool approved { get; set; }

        public virtual StakeholdersProjectEntity StakeholdersProject { get; set; }

        public String DisplayName
        {
            get
            {
                return this.StakeholdersProject.Stakeholders.Users.nickName;
            }
        }

    }
}
