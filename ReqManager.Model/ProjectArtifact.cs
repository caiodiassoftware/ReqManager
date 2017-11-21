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
        public int UserID { get; set; }
        public int ArtifactTypeID { get; set; }
        public int ImportanceID { get; set; }
        public int ProjectID { get; set; }
        [MaxLength(25), MinLength(4)]
        [Index(IsUnique = true)]
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
        public virtual Importance Importance { get; set; }
        public virtual Project Project { get; set; }
    }
}
