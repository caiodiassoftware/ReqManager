using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUIREMENT", Schema = "REQ")]
    public class Requirement
    {
        public Requirement()
        {
            this.LinkRequirementsOrigin = new HashSet<LinkBetweenRequirement>();
            this.LinkRequirementsTarget = new HashSet<LinkBetweenRequirement>();
            this.LinkRequirementsArtifacts = new HashSet<LinkBetweenRequirementsArtifacts>();
            this.StakeholderRequirement = new HashSet<StakeholderRequirement>();
            this.RequirementCharacteristics = new HashSet<RequirementCharacteristics>();
        }

        [Key]
        [Index("IX_REQUIREMENT", 1, IsUnique = true)]
        public int RequirementID { get; set; }
        public int ProjectID { get; set; }
        public int? RequirementTemplateID { get; set; }
        public int CreationUserID { get; set; }
        public int RequirementStatusID { get; set; }
        public int RequirementTypeID { get; set; }
        public int? RequirementSubTypeID { get; set; }
        public int ImportanceID { get; set; }
        [Index("IX_REQUIREMENT", 2, IsUnique = true)]
        public int versionNumber { get; set; }
        [MaxLength(25)]
        [Index(IsUnique = true)]
        public string code { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        [MaxLength(100), MinLength(10)]
        public string title { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        public Nullable<DateTime> startDate { get; set; }
        public Nullable<DateTime> endDate { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool preTraceability { get; set; }
        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal cost { get; set; }
        [Required]
        public bool active { get; set; }

        public virtual ICollection<RequirementVersions> RequirementVersions { get; set; }
        public virtual ICollection<RequirementCharacteristics> RequirementCharacteristics { get; set; }
        public virtual ICollection<LinkBetweenRequirement> LinkRequirementsOrigin { get; set; }
        public virtual ICollection<LinkBetweenRequirement> LinkRequirementsTarget { get; set; }
        public virtual ICollection<LinkBetweenRequirementsArtifacts> LinkRequirementsArtifacts { get; set; }
        public virtual ICollection<StakeholderRequirement> StakeholderRequirement { get; set; }
        public virtual Importance Importance { get; set; }
        [ForeignKey("CreationUserID")]
        public virtual Users Users { get; set; }
        public virtual RequirementTemplate RequirementTemplate { get; set; }
        public virtual RequirementStatus RequirementStatus { get; set; }
        public virtual RequirementType RequirementType { get; set; }
        public virtual RequirementSubType RequirementSubType { get; set; }
        public virtual Project Project { get; set; }
    }
}
