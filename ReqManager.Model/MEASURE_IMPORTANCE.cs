using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("MEASURE_IMPORTANCE", Schema = "PROJ")]
    public class MEASURE_IMPORTANCE
    {
        public MEASURE_IMPORTANCE()
        {
            this.ProjectArtifact = new HashSet<PROJECT_ARTIFACT>();
            this.Requirement = new HashSet<REQUIREMENT>();
            this.RequirementRationale = new HashSet<REQUIREMENT_RATIONALE>();
            this.Task = new HashSet<TASK>();
        }
    
        [Key]
        public int MeasureImportanceID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string description { get; set; }
    
        public virtual ICollection<PROJECT_ARTIFACT> ProjectArtifact { get; set; }
        public virtual ICollection<REQUIREMENT> Requirement { get; set; }
        public virtual ICollection<REQUIREMENT_RATIONALE> RequirementRationale { get; set; }
        public virtual ICollection<TASK> Task { get; set; }
    }
}
