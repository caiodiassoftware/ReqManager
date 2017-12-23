using ReqManager.Entities.Project;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementVersionsEntity
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
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        [Required]
        [MaxLength(1000), MinLength(10)]
        [DataType(DataType.MultilineText)]
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

        public String DisplayName
        {
            get
            {
                return RequirementRequestForChanges.StakeholderRequirement.Requirement.code + " v" + versionNumber;
            }
        }

        public virtual RequirementEntity Requirement { get; set; }
        public virtual RequirementRequestForChangesEntity RequirementRequestForChanges { get; set; }
        public virtual ImportanceEntity Importance { get; set; }
        public virtual RequirementTemplateEntity RequirementTemplate { get; set; }
        public virtual RequirementStatusEntity RequirementStatus { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }
        public virtual RequirementSubTypeEntity RequirementSubType { get; set; }
    }
}
