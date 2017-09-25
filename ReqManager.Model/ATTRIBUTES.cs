using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("ATTRIBUTES", Schema = "LINK")]
    public class Attributes
    {
        public Attributes()
        {
            this.LinkArtifactAttributes = new HashSet<LinkArtifactAttributes>();
            this.LinkRequirementAttributes = new HashSet<LinkRequirementAttributes>();
            this.AttributeTypeLink = new HashSet<AttributesTypeLink>();
        }
    
        [Key]
        public int AttributeID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Index(IsUnique = true)]
        public string description { get; set; }
    
        public virtual ICollection<LinkArtifactAttributes> LinkArtifactAttributes { get; set; }
        public virtual ICollection<LinkRequirementAttributes> LinkRequirementAttributes { get; set; }
        public virtual ICollection<AttributesTypeLink> AttributeTypeLink { get; set; }
    }
}
