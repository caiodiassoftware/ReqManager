namespace ReqManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("STAKEHOLDERS_PROJECT", Schema = "PROJ")]
    public class StakeholdersProject
    {
        public StakeholdersProject()
        {
            this.Requirement = new HashSet<Requirement>();
            this.RequirementRationale = new HashSet<RequirementRationale>();
        }
    
        [Key]
        public int StakeholdersProjectID { get; set; }
        public int ProjectID { get; set; }
        public int StakeholderID { get; set; }
        public DateTime creationDate { get; set; }

        public virtual Project Project { get; set; }
        public virtual Stakeholders Stakeholders { get; set; }
        public virtual ICollection<Requirement> Requirement { get; set; }
        public virtual ICollection<RequirementRationale> RequirementRationale { get; set; }
    }
}
