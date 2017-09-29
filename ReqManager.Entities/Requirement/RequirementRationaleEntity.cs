using ReqManager.Entities.Acess;
using ReqManager.Entities.Project;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementRationaleEntity
    {
        [Key]
        public int RequirementRationaleID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Stakeholder")]
        public int StakeholdersProjectID { get; set; }
        [Required]
        [Display(Name = "Importance")]
        public int MeasureImportanceID { get; set; }
        [Required]
        [Display(Name = "Requirement")]
        public int RequirementID { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int RequirementTypeID { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int RequirementStatusID { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required]
        [MaxLength(100), MinLength(10)]
        [Display(Name = "Title")]
        public string title { get; set; }
        [Required]
        [Display(Name = "Changed Date")]
        public System.DateTime changedDate { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        [Display(Name = "Input")]
        public string input { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        [Display(Name = "Output")]
        public string output { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        [Display(Name = "Rationale")]
        public string rationale { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual StakeholdersProjectEntity StakeholdersProject { get; set; }
        public virtual MeasureImportanceEntity MeasureImportance { get; set; }
        public virtual RequirementEntity Requirement { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }
        public virtual RequirementStatusEntity RequirementStatus { get; set; }
    }
}
