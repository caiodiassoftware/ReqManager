using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUIREMENT_RATIONALE", Schema = "REQ")]
    public partial class RequirementRationale
    {
        [Key]
        public int RequirementRationaleID { get; set; }
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
    
        public virtual Users User { get; set; }
        public virtual StakeholdersProject StakeholdersProject { get; set; }
        public virtual MeasureImportance MeasureImportance { get; set; }
        public virtual Requirement Requirement { get; set; }
        public virtual RequirementTask RequirementType { get; set; }
        public virtual RequirementStatus RequirementStatus { get; set; }
    }
}
