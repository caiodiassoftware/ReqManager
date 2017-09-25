using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{   
    [Table("LINK_ARTIFACT_ATTRIBUTES", Schema = "LINK")]
    public class LinkArtifactAttributes
    {
        [Key]
        public int ArtefactAttributeID { get; set; }
        [Index("IX_attribute_requirement", 1, IsUnique = true)]
        public int AttributeID { get; set; }
        [Index("IX_attribute_requirement", 2, IsUnique = true)]
        public int LinkArtifactRequirementID { get; set; }
        [Required]
        public string value { get; set; }
    
        public virtual Attributes Attributes { get; set; }
        public virtual LinkBetweenRequirementsArtifacts LinkRequirementsArtifacts { get; set; }
    }
}
