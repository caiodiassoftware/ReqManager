using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("ATTRIBUTES", Schema = "LINK")]
    public class ATTRIBUTES
    {
        public ATTRIBUTES()
        {
            this.LinkArtifactAttributes = new HashSet<LINK_ARTIFACT_ATTRIBUTES>();
            this.LinkRequirementAttributes = new HashSet<LINK_REQUIREMENT_ATTRIBUTES>();
            this.AttributeTypeLink = new HashSet<ATTRIBUTES_TYPE_LINK>();
        }
    
        [Key]
        public int AttributeID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string description { get; set; }
    
        public virtual ICollection<LINK_ARTIFACT_ATTRIBUTES> LinkArtifactAttributes { get; set; }
        public virtual ICollection<LINK_REQUIREMENT_ATTRIBUTES> LinkRequirementAttributes { get; set; }
        public virtual ICollection<ATTRIBUTES_TYPE_LINK> AttributeTypeLink { get; set; }
    }
}
