using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("LINK_BETWEEN_REQUIREMENTS_ARTIFACTS", Schema = "LINK")]
    public class LINK_BETWEEN_REQUIREMENTS_ARTIFACTS
    {
        public LINK_BETWEEN_REQUIREMENTS_ARTIFACTS()
        {
            this.LinkArtifactAttributes = new HashSet<LINK_ARTIFACT_ATTRIBUTES>();
        }
    
        [Key]
        public int LinkArtifactRequirementID { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25)]
        public string code { get; set; }
    
        public virtual USERS Users { get; set; }
        public virtual PROJECT_ARTIFACT ProjectArtifact { get; set; }
        public virtual REQUIREMENT Requirement { get; set; }
        public virtual ICollection<LINK_ARTIFACT_ATTRIBUTES> LinkArtifactAttributes { get; set; }        
        public virtual TYPE_LINK TypeLink { get; set; }
    }
}
