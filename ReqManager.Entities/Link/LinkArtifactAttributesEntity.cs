using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Link
{
    public class LinkArtifactAttributesEntity
    {
        [Key]
        public int ArtefactAttributeID { get; set; }
        [Required]
        [Display(Name = "Attributo")]
        public int AttributeID { get; set; }        
        [Required]
        [Display(Name = "Artefact - Requirement")]
        public int LinkArtifactRequirementID { get; set; }
        [Required]
        [Display(Name = "Value")]
        public string value { get; set; }

        public virtual AttributesEntity Attributes { get; set; }
        public virtual LinkBetweenRequirementsArtifactsEntity LinkRequirementsArtifacts { get; set; }
    }
}
