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
        public string value { get; set; }
    
        public virtual Attributes Attributes { get; set; }
        public virtual LinkBetweenRequirementsArtifacts LinkRequirementsArtifacts { get; set; }
    }
}
