using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("MEASURE_IMPORTANCE", Schema = "PROJ")]
    public class MeasureImportance
    {
        public MeasureImportance()
        {
            this.ProjectArtifact = new HashSet<ProjectArtifact>();
            this.Requirement = new HashSet<Requirement>();
            this.RequirementRationale = new HashSet<RequirementRationale>();
            this.Task = new HashSet<Task>();
        }
    
        [Key]
        public int MeasureImportanceID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Index(IsUnique = true)]
        public string description { get; set; }
    
        public virtual ICollection<ProjectArtifact> ProjectArtifact { get; set; }
        public virtual ICollection<Requirement> Requirement { get; set; }
        public virtual ICollection<RequirementRationale> RequirementRationale { get; set; }
        public virtual ICollection<Task> Task { get; set; }
    }
}
