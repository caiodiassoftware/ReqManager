using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("LINK_BETWEEN_REQUIREMENT", Schema = "LINK")]
    public class LINK_BETWEEN_REQUIREMENT
    {
        public LINK_BETWEEN_REQUIREMENT()
        {
            this.linkRequirementAttributes = new HashSet<LINK_REQUIREMENT_ATTRIBUTES>();
        }
    
        [Key]
        public int LinkRequirementsID { get; set; }
        [Required]  
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25), MinLength(5)]
        public string code { get; set; }
    
        public virtual USERS Users { get; set; }
        public virtual ICollection<LINK_REQUIREMENT_ATTRIBUTES> linkRequirementAttributes { get; set; }
        public virtual REQUIREMENT RequirementOriginID { get; set; }        
        public virtual REQUIREMENT RequirementTargetID { get; set; }
        public virtual TYPE_LINK TypeLink { get; set; }
    }
}
