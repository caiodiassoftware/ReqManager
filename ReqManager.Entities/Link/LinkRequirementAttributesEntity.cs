using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Link
{
    public class LinkRequirementAttributesEntity
    {
        [Key]
        public int RequirementAttributeID { get; set; }
        [Required]
        [Display(Name = "Attribute")]
        public int AttributeID { get; set; }
        [Required]
        [Display(Name = "Requeriment - Requirement")]
        public int LinkRequirementID { get; set; }
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
