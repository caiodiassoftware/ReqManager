using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class StakeholdersProjectEntity
    {
        [Key]
        [Display(Name = "Stakeholder Project")]
        public int StakeholdersProjectID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Stakeholder")]
        public int StakeholderID { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; } = DateTime.Now;

        [Display(Name = "Stakeholder")]
        public String DisplayName
        {
            get
            {
                return this.Project.code + " - " + this.Stakeholders.DisplayName;
            }
        }

        public virtual ProjectEntity Project { get; set; }
        public virtual StakeholdersEntity Stakeholders { get; set; }
    }
}
