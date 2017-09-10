namespace ReqManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("STAKEHOLDERS_PROJECT", Schema = "PROJ")]
    public class STAKEHOLDERS_PROJECT
    {
        public STAKEHOLDERS_PROJECT()
        {
            this.Requirement = new HashSet<REQUIREMENT>();
            this.RequirementRationale = new HashSet<REQUIREMENT_RATIONALE>();
        }
    
        [Key]
        public int StakeholdersProjectID { get; set; }
        public DateTime creationDate { get; set; }
    
        public virtual USERS User { get; set; }
        public virtual PROJECT Project { get; set; }
        public virtual STAKEHOLDERS Stakeholders { get; set; }
        public virtual ICollection<REQUIREMENT> Requirement { get; set; }
        public virtual ICollection<REQUIREMENT_RATIONALE> RequirementRationale { get; set; }
    }
}
