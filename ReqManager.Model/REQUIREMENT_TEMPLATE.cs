using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{

    [Table("REQUIREMENT_TEMPLATE", Schema = "REQ")]
    public class REQUIREMENT_TEMPLATE
    {
        public REQUIREMENT_TEMPLATE()
        {
            this.Requirement = new HashSet<REQUIREMENT>();
        }
    
        [Key]
        public int RequirementTemplateID { get; set; }
        [Required]
        [MaxLength(50), MinLength(6)]
        public string description { get; set; }
        [Required]
        public string templateHtml { get; set; }
        public DateTime createDate { get; set; }
    
        public virtual USERS Users { get; set; }
        public virtual ICollection<REQUIREMENT> Requirement { get; set; }
        public virtual REQUIREMENT_TYPE RequirementType { get; set; }
    }
}
