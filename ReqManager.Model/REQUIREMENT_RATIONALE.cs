using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUIREMENT_RATIONALE", Schema = "REQ")]
    public partial class REQUIREMENT_RATIONALE
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
    
        public virtual USERS User { get; set; }
        public virtual STAKEHOLDERS_PROJECT StakeholdersProject { get; set; }
        public virtual MEASURE_IMPORTANCE MeasureImportance { get; set; }
        public virtual REQUIREMENT Requirement { get; set; }
        public virtual REQUIREMENT_TYPE RequirementType { get; set; }
        public virtual REQUIREMENT_STATUS RequirementStatus { get; set; }
    }
}
