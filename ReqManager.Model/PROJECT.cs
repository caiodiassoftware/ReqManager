using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("PROJECT", Schema = "PROJ")]
    public class PROJECT
    {
        public PROJECT()
        {
            this.ProjectArtifact = new HashSet<PROJECT_ARTIFACT>();
            this.HistoryProject = new HashSet<HISTORY_PROJECT>();
            this.StakeholderProject = new HashSet<STAKEHOLDERS_PROJECT>();
            this.ProjectRequirement = new HashSet<PROJECT_REQUIREMENTS>();
        }
    
        [Key]
        public int ProjectID { get; set; }
        [Required]
        [MaxLength(255)]
        public string description { get; set; }
        [Required]
        [MaxLength(300)]
        public string pathForTraceability { get; set; }
        [Required]
        [MaxLength(1000)]
        public string environmentalInformation { get; set; }
        [Required]
        [MaxLength(1000)]
        public string managementInformation { get; set; }
        [Required]
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        [Required]
        public System.DateTime creationDate { get; set; }
    
        public virtual USERS Users { get; set; }
        public virtual ICollection<PROJECT_ARTIFACT> ProjectArtifact { get; set; }
        public virtual ICollection<HISTORY_PROJECT> HistoryProject { get; set; }
        public virtual PROJECT_PHASES ProjectPhases { get; set; }
        public virtual ICollection<STAKEHOLDERS_PROJECT> StakeholderProject { get; set; }
        public virtual ICollection<PROJECT_REQUIREMENTS> ProjectRequirement { get; set; }
    }
}
