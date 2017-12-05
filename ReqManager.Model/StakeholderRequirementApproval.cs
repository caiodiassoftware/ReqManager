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
        [Key]
        public int StakeholderRequirementApprovalID { get; set; }
        public int StakeholdersProjectID { get; set; }
        public int RequirementID { get; set; }
        public DateTime creationDate { get; set; }
        [MinLength(6)]
        [MaxLength(255)]
        [DefaultValue("APPROVED")]
        public string description { get; set; }
        public bool approved { get; set; }

        public virtual StakeholdersProject StakeholdersProject { get; set; }
        public virtual Requirement Requirement { get; set; }
    }
}
