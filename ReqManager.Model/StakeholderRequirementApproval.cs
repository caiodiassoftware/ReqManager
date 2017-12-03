using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("STAKEHOLDER_REQUIREMENT_APPROVAL", Schema = "PROJ")]
    public class StakeholderRequirementApproval
    {
        public StakeholderRequirementApproval()
        {
            this.RequirementRequestForChanges = new HashSet<RequirementRequestForChanges>();
        }

        [Key]
        public int StakeholderRequirementApprovalID { get; set; }
        [Index("IX_STAKEHOLDER_REQUIREMENT_APPROVAL", 1, IsUnique = true)]
        public int StakeholdersProjectID { get; set; }
        [Index("IX_STAKEHOLDER_REQUIREMENT_APPROVAL", 2, IsUnique = true)]
        public int RequirementID { get; set; }
        public DateTime creationDate { get; set; }
        [MinLength(6)]
        [MaxLength(255)]
        [DefaultValue("APPROVED")]
        public string description { get; set; }
        public bool approved { get; set; }

        public virtual ICollection<RequirementRequestForChanges> RequirementRequestForChanges { get; set; }
        public virtual StakeholdersProject StakeholdersProject { get; set; }
        public virtual Requirement Requirement { get; set; }
    }
}
