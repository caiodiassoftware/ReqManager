using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUIREMENT", Schema = "REQ")]
    public class REQUIREMENT
    {
        public REQUIREMENT()
        {
            this.LinkRequirementsOrigin = new HashSet<LINK_BETWEEN_REQUIREMENT>();
            this.LinkRequirementsTarget = new HashSet<LINK_BETWEEN_REQUIREMENT>();
            this.LinkRequirementsArtifacts = new HashSet<LINK_BETWEEN_REQUIREMENTS_ARTIFACTS>();
            this.ProjectRequirements = new HashSet<PROJECT_REQUIREMENTS>();
            this.RequirementActionHistory = new HashSet<REQUIREMENT_ACTION_HISTORY>();
            this.RequirementRationale = new HashSet<REQUIREMENT_RATIONALE>();
        }
    
        [Key]
        public int RequirementID { get; set; }
        public Nullable<int> RequirementTemplateID { get; set; }
        public int StakeholderID { get; set; }
        [Required]
        [MaxLength(25)]
        public string code { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        [MaxLength(100), MinLength(10)]
        public string title { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        public string input { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        public string output { get; set; }

        public virtual USERS Users { get; set; }
        [InverseProperty("RequirementOriginID")]
        public virtual ICollection<LINK_BETWEEN_REQUIREMENT> LinkRequirementsOrigin { get; set; }
        [InverseProperty("RequirementTargetID")]
        public virtual ICollection<LINK_BETWEEN_REQUIREMENT> LinkRequirementsTarget { get; set; }
        public virtual ICollection<LINK_BETWEEN_REQUIREMENTS_ARTIFACTS> LinkRequirementsArtifacts { get; set; }
        public virtual ICollection<PROJECT_REQUIREMENTS> ProjectRequirements { get; set; }
        public virtual STAKEHOLDERS_PROJECT StakeholderProject { get; set; }
        public virtual MEASURE_IMPORTANCE MeasureImportance { get; set; }
        public virtual ICollection<REQUIREMENT_ACTION_HISTORY> RequirementActionHistory { get; set; }
        public virtual ICollection<REQUIREMENT_RATIONALE> RequirementRationale { get; set; }
        public virtual REQUIREMENT_TEMPLATE RequirementTemplate { get; set; }
        public virtual REQUIREMENT_STATUS RequirementStatus { get; set; }
        public virtual REQUIREMENT_TYPE RequirementType { get; set; }
    }
}
