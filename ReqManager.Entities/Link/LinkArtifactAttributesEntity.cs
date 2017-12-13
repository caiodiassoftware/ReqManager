using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Link
{
    public class LinkArtifactAttributesEntity
    {
        [Key]
        [Display(Name = "Artifact Attribute")]
        public int ArtefactAttributeID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Attribute")]
        public int AttributeID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Artifact - Requirement")]
        public int LinkArtifactRequirementID { get; set; }
        [Required]
        [Display(Name = "Value")]
        public string value { get; set; }

        public String DisplayName
        {
            get
            {
                return this.LinkRequirementsArtifacts.code + " - " + this.Attributes.description +
                    " : " + this.value;
            }
        }

        public virtual AttributesEntity Attributes { get; set; }
        public virtual LinkBetweenRequirementsArtifactsEntity LinkRequirementsArtifacts { get; set; }
    }
}
