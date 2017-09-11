using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("LINK_REQUIREMENT_ATTRIBUTES", Schema = "LINK")]
    public partial class LinkRequirementAttributes
    {
        [Key]
        public int RequirementAttributeID { get; set; }
        [Required]
        public string value { get; set; }
    
        public virtual Attributes Attributes { get; set; }
        public virtual LinkBetweenRequirement LinkRequirement { get; set; }
    }
}
