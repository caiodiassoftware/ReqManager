using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("PROJECT", Schema = "PROJ")]
    public class Project
    {
        public Project()
        {
            this.ProjectArtifact = new HashSet<ProjectArtifact>();
            this.HistoryProject = new HashSet<HistoryProject>();
            this.StakeholderProject = new HashSet<StakeholdersProject>();
            this.ProjectRequirement = new HashSet<ProjectRequirements>();
        }
    
        [Key]
        public int ProjectID { get; set; }
        public int UserID { get; set; }
        public int ProjectPhasesID { get; set; }
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
    
        public virtual Users Users { get; set; }
        public virtual ICollection<ProjectArtifact> ProjectArtifact { get; set; }
        public virtual ICollection<HistoryProject> HistoryProject { get; set; }
        public virtual ProjectPhases ProjectPhases { get; set; }
        public virtual ICollection<StakeholdersProject> StakeholderProject { get; set; }
        public virtual ICollection<ProjectRequirements> ProjectRequirement { get; set; }
    }
}
