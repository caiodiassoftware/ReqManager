using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class StakeholdersProjectEntity
    {
        [Key]
        public int StakeholdersProjectID { get; set; }
        [Required]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Required]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Stakeholder")]
        public int StakeholderID { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(255)]
        public string description { get; set; }
        [Required]
        [Range(1, 9)]
        [Display(Name = "Importance to Project")]
        public int importanceValue { get; set; }

        public virtual ProjectEntity Project { get; set; }
        public virtual StakeholdersEntity Stakeholders { get; set; }

        [Display(Name = "Stakeholders")]
        public String DisplayName
        {
            get
            {
                return this.Stakeholders.DisplayName;
            }
        }
    }
}
