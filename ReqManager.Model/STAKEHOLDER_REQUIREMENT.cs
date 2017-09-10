using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("STAKEHOLDER_REQUIREMENT", Schema = "PROJ")]
    public class STAKEHOLDER_REQUIREMENT
    {
        [Key]
        public int StakeHolderRequirementID { get; set; }
        public DateTime creationDate { get; set; }
    
        public virtual USERS User { get; set; }
        public virtual PROJECT_REQUIREMENTS ProjectRequirements { get; set; }
        public virtual STAKEHOLDERS StakeHolders { get; set; }
    }
}
