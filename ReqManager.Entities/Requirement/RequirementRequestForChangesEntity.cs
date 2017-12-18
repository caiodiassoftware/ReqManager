using ReqManager.Entities.Project;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementRequestForChangesEntity
    {
        [Key]
        public int RequirementRequestForChangesID { get; set; }
        public int StakeholderRequirementID { get; set; }
        public int RequestStatusID { get; set; }
        [Required]
        [MaxLength(1000), MinLength(10)]
        [DataType(DataType.MultilineText)]
        public string request { get; set; }
        [Required]
        public DateTime creationDate { get; set; }

        public virtual StakeholderRequirementEntity StakeholderRequirement { get; set; }
        public virtual RequestStatusEntity RequestStatus { get; set; }
    }
}
