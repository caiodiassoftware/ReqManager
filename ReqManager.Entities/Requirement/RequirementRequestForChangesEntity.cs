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
        [Required]
        [MaxLength(1000), MinLength(10)]
        public string request { get; set; }
        [Required]
        public DateTime creationDate { get; set; }

        public virtual RequirementEntity Requirement { get; set; }
        public virtual StakeholderRequirementEntity StakeholderRequirement { get; set; }
    }
}
