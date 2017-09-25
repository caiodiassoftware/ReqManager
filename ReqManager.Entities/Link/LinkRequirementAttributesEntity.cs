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
        public int AttributeID { get; set; }
        [Required]
        public int LinkRequirementID { get; set; }
        [Required]
        public string value { get; set; }

        public virtual AttributesEntity Attributes { get; set; }
        public virtual LinkBetweenRequirementsEntity LinkRequirement { get; set; }
    }
}
