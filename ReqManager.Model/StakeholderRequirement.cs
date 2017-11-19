using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("STAKEHOLDER_REQUIREMENT", Schema = "PROJ")]
    public class StakeholderRequirement
    {
        [Key]
        public int StakeHolderRequirementID { get; set; }
        [Index("IX_STAKEHOLDER_REQUIREMENT", 1, IsUnique = true)]
        public int ProjectRequirementID { get; set; }
        [Index("IX_STAKEHOLDER_REQUIREMENT", 2, IsUnique = true)]
        public int StakeHolderID { get; set; }
        public DateTime creationDate { get; set; }
        [MinLength(6)]
        [MaxLength(255)]
        [DefaultValue("APPROVED")]
        public string description { get; set; }
        public bool approved { get; set; }

        public virtual ProjectRequirements ProjectRequirements { get; set; }
        public virtual Stakeholders StakeHolders { get; set; }
    }
}
