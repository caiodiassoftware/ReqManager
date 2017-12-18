using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("STAKEHOLDER_REQUIREMENT_APPROVAL", Schema = "REQ")]
    public class StakeholderRequirementApproval
    {
        [Key]
        public int StakeholderRequirementApprovalID { get; set; }
        public int StakeholderRequirementID { get; set; }
        public DateTime creationDate { get; set; }
        [MinLength(6)]
        [MaxLength(1000)]
        [Required]
        public string description { get; set; }
        public bool approved { get; set; }

        public virtual StakeholderRequirement StakeholderRequirement { get; set; }
    }
}
