using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("STAKEHOLDER_REQUIREMENT", Schema = "PROJ")]
    public class StakeholderRequirement
    {
        public StakeholderRequirement()
        {
            this.RequirementRequestForChanges = new HashSet<RequirementRequestForChanges>();
            this.StakeholderRequirementApproval = new HashSet<StakeholderRequirementApproval>();
        }

        [Key]
        public int StakeholderRequirementID { get; set; }
        public int StakeholdersProjectID { get; set; }
        public int RequirementID { get; set; }
        public DateTime creationDate { get; set; }
        [Required]
        [Range(0, 9)]
        public int importanceValue { get; set; }

        public virtual StakeholdersProject StakeholdersProject { get; set; }
        public virtual Requirement Requirement { get; set; }
        public virtual ICollection<RequirementRequestForChanges> RequirementRequestForChanges { get; set; }
        public virtual ICollection<StakeholderRequirementApproval> StakeholderRequirementApproval { get; set; }
    }
}
