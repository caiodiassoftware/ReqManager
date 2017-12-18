using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class StakeholderRequirementApprovalEntity
    {
        [Key]
        public int StakeholderRequirementApprovalID { get; set; }
        [Range(1, Double.PositiveInfinity)]
        public int StakeholderRequirementID { get; set; }
        public DateTime creationDate { get; set; } = DateTime.Now;
        [MinLength(6)]
        [MaxLength(1000)]
        [Required]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        public bool approved { get; set; }

        public virtual StakeholderRequirementEntity StakeholderRequirement { get; set; }

        public String DisplayName
        {
            get
            {
                return this.StakeholderRequirement.StakeholdersProject.Stakeholders.Users.nickName;
            }
        }

    }
}
