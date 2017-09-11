using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("PROJECT_ARTIFACT", Schema = "PROJ")]
    public class ProjectArtifact
    {
        public ProjectArtifact()
        {
            this.HistoryProjectArtifact = new HashSet<HistoryProjectArtifact>();
            this.LinkRequirementArtifact = new HashSet<LinkBetweenRequirementsArtifacts>();
        }
    
        [Key]
        public int ProjectArtifactID { get; set; }
        [Required]
        [MaxLength(25), MinLength(5)]
        public string code { get; set; }
        [Required]
        [MaxLength(500)]
        public string path { get; set; }
        [Required]
        [MaxLength(500)]
        public string description { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual ArtifactType ArtifactType { get; set; }
        public virtual ICollection<HistoryProjectArtifact> HistoryProjectArtifact { get; set; }
        public virtual ICollection<LinkBetweenRequirementsArtifacts> LinkRequirementArtifact { get; set; }
        public virtual MeasureImportance MeasureImportance { get; set; }
        public virtual Project Project { get; set; }
    }
}
