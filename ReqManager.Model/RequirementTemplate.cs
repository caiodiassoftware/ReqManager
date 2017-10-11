using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{

    [Table("REQUIREMENT_TEMPLATE", Schema = "REQ")]
    public class RequirementTemplate
    {
        public RequirementTemplate()
        {
            this.Requirement = new HashSet<Requirement>();
        }
    
        [Key]
        public int RequirementTemplateID { get; set; }
        public int UserID { get; set; }
        public int RequirementTypeID { get; set; }
        [Required]
        [MaxLength(50), MinLength(6)]
        public string description { get; set; }
        [Required]
        public string templateHtml { get; set; }
        [Required]
        public DateTime createDate { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual ICollection<Requirement> Requirement { get; set; }
        public virtual RequirementType RequirementType { get; set; }
    }
}
