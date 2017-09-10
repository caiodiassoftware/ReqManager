using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{   
    [Table("LINK_ARTIFACT_ATTRIBUTES", Schema = "LINK")]
    public class LINK_ARTIFACT_ATTRIBUTES
    {
        [Key]
        public int ArtefactAttributeID { get; set; }
        public string value { get; set; }
    
        public virtual ATTRIBUTES Attributes { get; set; }
        public virtual LINK_BETWEEN_REQUIREMENTS_ARTIFACTS LinkRequirementsArtifacts { get; set; }
    }
}
