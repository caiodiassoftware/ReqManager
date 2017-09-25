using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("ARTIFACT_TYPE", Schema = "ART")]
    public class ArtifactType
    {
        public ArtifactType()
        {
            this.ProjectArtifact = new HashSet<ProjectArtifact>();
        }
    
        [Key]
        public int ArtefactTypeID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Index(IsUnique = true)]
        public string description { get; set; }
    
        public virtual ICollection<ProjectArtifact> ProjectArtifact { get; set; }
    }
}
