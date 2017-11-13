using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Link
{
    public class LinkRequirementAttributesEntity
    {
        [Key]
        [Display(Name = "R to R Attributes")]
        public int RequirementAttributeID { get; set; }
        [Required]
        [Display(Name = "Attribute")]
        public int AttributeID { get; set; }
        [Required]
        [Display(Name = "R to R")]
        public int LinkRequirementsID { get; set; }
        [Required]
        [Display(Name = "Value")]
        public string value { get; set; }

        public String DisplayName
        {
            get
            {
                return this.Attributes.description + " - " + this.LinkRequirement.code + " - " + this.value;
            }
        }

        public virtual AttributesEntity Attributes { get; set; }
        public virtual LinkBetweenRequirementsEntity LinkRequirement { get; set; }
    }
}
