namespace ReqManager.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("STAKEHOLDERS", Schema = "PROJ")]
    public class Stakeholders
    {
        public Stakeholders()
        {
            this.StakeHoldersProject = new HashSet<StakeholdersProject>();
        }
    
        [Key]
        public int StakeholderID { get; set; }
        [Index("IX_STAKEHOLDERS", 1, IsUnique = true)]
        public int UserID { get; set; }
        [Index("IX_STAKEHOLDERS", 2, IsUnique = true)]
        public int ClassificationID { get; set; }

        public virtual Users Users { get; set; }
        public virtual StakeholderClassification StakeHolderClassification { get; set; }
        public virtual ICollection<StakeholdersProject> StakeHoldersProject { get; set; }
    }
}
