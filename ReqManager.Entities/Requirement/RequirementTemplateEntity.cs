using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Requirement
{
    public class RequirementTemplateEntity
    {
        [Key]
        public int RequirementTemplateID { get; set; }
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Display(Name = "Type")]
        public int RequirementTypeID { get; set; }
        [Required]
        [MaxLength(50), MinLength(6)]
        [Display(Name = "Template")]
        public string description { get; set; }
        [Display(Name = "Template Html")]
        public string templateHtml { get; set; }
        [Display(Name = "Creation Date")]
        public System.DateTime createDate { get; set; } = DateTime.Now;

        public virtual UserEntity Users { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }
    }
}
