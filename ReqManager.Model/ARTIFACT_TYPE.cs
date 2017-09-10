using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("ARTIFACT_TYPE", Schema = "ART")]
    public class ARTIFACT_TYPE
    {
        public ARTIFACT_TYPE()
        {
            this.ProjectArtifact = new HashSet<PROJECT_ARTIFACT>();
        }
    
        [Key]
        public int ArtefactTypeID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string description { get; set; }
    
        public virtual ICollection<PROJECT_ARTIFACT> ProjectArtifact { get; set; }
    }
}
