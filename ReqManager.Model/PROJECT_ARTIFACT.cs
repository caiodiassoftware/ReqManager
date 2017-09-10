using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("PROJECT_ARTIFACT", Schema = "PROJ")]
    public class PROJECT_ARTIFACT
    {
        public PROJECT_ARTIFACT()
        {
            this.HistoryProjectArtifact = new HashSet<HISTORY_PROJECT_ARTIFACT>();
            this.LinkRequirementArtifact = new HashSet<LINK_BETWEEN_REQUIREMENTS_ARTIFACTS>();
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
    
        public virtual USERS Users { get; set; }
        public virtual ARTIFACT_TYPE ArtifactType { get; set; }
        public virtual ICollection<HISTORY_PROJECT_ARTIFACT> HistoryProjectArtifact { get; set; }
        public virtual ICollection<LINK_BETWEEN_REQUIREMENTS_ARTIFACTS> LinkRequirementArtifact { get; set; }
        public virtual MEASURE_IMPORTANCE MeasureImportance { get; set; }
        public virtual PROJECT Project { get; set; }
    }
}
