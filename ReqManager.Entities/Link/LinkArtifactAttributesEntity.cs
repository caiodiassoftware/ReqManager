using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Link
{
    public class LinkArtifactAttributesEntity
    {
        [Key]
        [Display(Name = "Artifact Attribute")]
        public int ArtefactAttributeID { get; set; }
        [Required]
        [Display(Name = "Attribute")]
        public int AttributeID { get; set; }        
        [Required]
        [Display(Name = "Artefact - Requirement")]
        public int LinkArtifactRequirementID { get; set; }
        [Required]
        [Display(Name = "Value")]
        public string value { get; set; }

        public String DisplayName
        {
            get
            {
                return this.Attributes.description + " - " + this.LinkRequirementsArtifacts.code;
            }
        }

        public virtual AttributesEntity Attributes { get; set; }
        public virtual LinkBetweenRequirementsArtifactsEntity LinkRequirementsArtifacts { get; set; }
    }
}
