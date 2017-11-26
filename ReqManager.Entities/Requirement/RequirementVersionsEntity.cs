using ReqManager.Entities.Acess;
using ReqManager.Entities.Project;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementVersionsEntity
    {
        [Key]
        public int RequirementVersionsID { get; set; }
        public int RequirementRequestForChangesID { get; set; }
        public int RequirementTypeID { get; set; }
        public Nullable<int> RequirementSubTypeID { get; set; }
        public int ImportanceID { get; set; }
        public int RequirementStatusID { get; set; }
        public int RequirementTemplateID { get; set; }
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

        public String DisplayName
        {
            get
            {
                return RequirementRequestForChanges.Requirement.code + " v" + versionNumber;
            }
        }

        public virtual RequirementRequestForChangesEntity RequirementRequestForChanges { get; set; }
        public virtual ImportanceEntity Importance { get; set; }
        public virtual RequirementTemplateEntity RequirementTemplate { get; set; }
        public virtual RequirementStatusEntity RequirementStatus { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }
        public virtual RequirementSubTypeEntity RequirementSubType { get; set; }
    }
}
