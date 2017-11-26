using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUIREMENT_REQUEST_FOR_CHANGES", Schema = "REQ")]
    public class RequirementRequestForChanges
    {
        [Key]
        public int RequirementRequestForChangesID { get; set; }
        public int RequirementID { get; set; }
        public int StakeHolderRequirementID { get; set; }
        public int RequestStatusID { get; set; }
        [Required]
        [MaxLength(1000), MinLength(10)]
        public string request { get; set; }
        [Required]
        public DateTime creationDate { get; set; }

        public virtual Requirement Requirement { get; set; }
        public virtual StakeholderRequirement StakeholderRequirement { get; set; }
        public virtual RequestStatus RequestStatus { get; set; }
    }
}
