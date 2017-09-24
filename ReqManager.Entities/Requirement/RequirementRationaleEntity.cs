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
        public int UserID { get; set; }
        [Required]
        public int StakeholdersProjectID { get; set; }
        [Required]
        public int MeasureImportanceID { get; set; }
        [Required]
        public int RequirementID { get; set; }
        [Required]
        public int RequirementTypeID { get; set; }
        [Required]
        public int RequirementStatusID { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        [MaxLength(100), MinLength(10)]
        public string title { get; set; }
        [Required]
        public DateTime changedDate { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        public string input { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        public string output { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        public string rationale { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual StakeholdersProjectEntity StakeholdersProject { get; set; }
        public virtual MeasureImportanceEntity MeasureImportance { get; set; }
        public virtual RequirementEntity Requirement { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }
        public virtual RequirementStatusEntity RequirementStatus { get; set; }
    }
}
