using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUIREMENT_VERSIONS", Schema = "REQ")]
    public partial class RequirementVersions
    {
        [Key]
        public int RequirementVersionsID { get; set; }
        public Nullable<int> RequirementRequestForChangesID { get; set; }
        public Nullable<int> RequirementID { get; set; }
        public int RequirementTypeID { get; set; }
        public Nullable<int> RequirementSubTypeID { get; set; }
        public int ImportanceID { get; set; }
        public int RequirementStatusID { get; set; }
        public Nullable<int> RequirementTemplateID { get; set; }
        [Required]
        public int versionNumber { get; set; }
        [Required]
        [MaxLength(100), MinLength(10)]
        public string title { get; set; }
        [Required]
        public string description { get; set; }        
        [Required]
        [MaxLength(1000), MinLength(10)]
        public string rationale { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        public Nullable<DateTime> startDate { get; set; }
        public Nullable<DateTime> endDate { get; set; }
        [Required]
        public bool preTraceability { get; set; }
        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal cost { get; set; }
        [Required]
        public bool active { get; set; }

        public virtual RequirementRequestForChanges RequirementRequestForChanges { get; set; }
        public virtual Importance Importance { get; set; }
        public virtual Requirement Requirement { get; set; }
        public virtual RequirementTemplate RequirementTemplate { get; set; }
        public virtual RequirementStatus RequirementStatus { get; set; }
        public virtual RequirementType RequirementType { get; set; }
        public virtual RequirementSubType RequirementSubType { get; set; }
    }
}
