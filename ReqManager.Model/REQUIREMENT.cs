using System;
using System.Collections.Generic;
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
            this.ProjectRequirements = new HashSet<ProjectRequirements>();
            this.RequirementActionHistory = new HashSet<RequirementActionHistory>();
            this.RequirementRationale = new HashSet<RequirementRationale>();
        }
    
        [Key]
        public int RequirementID { get; set; }
        public Nullable<int> RequirementTemplateID { get; set; }
        public int StakeholderID { get; set; }
        public int UserID { get; set; }
        public int RequirementStatusID { get; set; }
        public int RequirementTypeID { get; set; }
        public int StakeholderProjectID { get; set; }
        public int MeasureImportanceID { get; set; }
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

        public virtual Users Users { get; set; }
        [InverseProperty("RequirementOriginID")]
        public virtual ICollection<LinkBetweenRequirement> LinkRequirementsOrigin { get; set; }
        [InverseProperty("RequirementTargetID")]
        public virtual ICollection<LinkBetweenRequirement> LinkRequirementsTarget { get; set; }
        public virtual ICollection<LinkBetweenRequirementsArtifacts> LinkRequirementsArtifacts { get; set; }
        public virtual ICollection<ProjectRequirements> ProjectRequirements { get; set; }
        public virtual StakeholdersProject StakeholderProject { get; set; }
        public virtual MeasureImportance MeasureImportance { get; set; }
        public virtual ICollection<RequirementActionHistory> RequirementActionHistory { get; set; }
        public virtual ICollection<RequirementRationale> RequirementRationale { get; set; }
        public virtual RequirementTemplate RequirementTemplate { get; set; }
        public virtual RequirementStatus RequirementStatus { get; set; }
        public virtual RequirementType RequirementType { get; set; }
    }
}
