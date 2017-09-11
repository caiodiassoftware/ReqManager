using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("STAKEHOLDER_REQUIREMENT", Schema = "PROJ")]
    public class StakeholderRequirement
    {
        [Key]
        public int StakeHolderRequirementID { get; set; }
        public DateTime creationDate { get; set; }
    
        public virtual Users User { get; set; }
        public virtual ProjectRequirements ProjectRequirements { get; set; }
        public virtual Stakeholders StakeHolders { get; set; }
    }
}
