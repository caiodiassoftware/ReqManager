using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("LINK_BETWEEN_REQUIREMENT", Schema = "LINK")]
    public class LinkBetweenRequirement
    {
        public LinkBetweenRequirement()
        {
            this.linkRequirementAttributes = new HashSet<LinkRequirementAttributes>();
        }
    
        [Key]
        public int LinkRequirementsID { get; set; }
        [Required]  
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25), MinLength(5)]
        public string code { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual ICollection<LinkRequirementAttributes> linkRequirementAttributes { get; set; }
        public virtual Requirement RequirementOriginID { get; set; }        
        public virtual Requirement RequirementTargetID { get; set; }
        public virtual TypeLink TypeLink { get; set; }
    }
}
