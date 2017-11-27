using ReqManager.Entities.Project;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementRequestForChangesEntity
    {
        [Key]
        public int RequirementRequestForChangesID { get; set; }
        public int RequirementID { get; set; }
        public int StakeHolderRequirementID { get; set; }
        [Display(Name = "Request Status")]
        public int RequestStatusID { get; set; }
        [Required]
        [MaxLength(1000), MinLength(10)]
        [Display(Name = "Request")]
        public string request { get; set; }
        [Required]
        public DateTime creationDate { get; set; } = DateTime.Now;

        public virtual RequirementEntity Requirement { get; set; }
        public virtual StakeholderRequirementEntity StakeholderRequirement { get; set; }
        public virtual RequestStatusEntity RequestStatus { get; set; }
    }
}
