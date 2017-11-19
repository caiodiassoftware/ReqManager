using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("IMPORTANCE", Schema = "PROJ")]
    public class Importance
    {
        public Importance()
        {
            this.ProjectArtifact = new HashSet<ProjectArtifact>();
            this.Requirement = new HashSet<Requirement>();
            this.Task = new HashSet<Task>();
        }
    
        [Key]
        public int ImportanceID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Index(IsUnique = true)]
        public string description { get; set; }
    
        public virtual ICollection<ProjectArtifact> ProjectArtifact { get; set; }
        public virtual ICollection<Requirement> Requirement { get; set; }
        public virtual ICollection<Task> Task { get; set; }
    }
}
