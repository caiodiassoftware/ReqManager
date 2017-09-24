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
        public int UserID { get; set; }
        [Required]
        [MaxLength(50), MinLength(6)]
        public string description { get; set; }
        [Required]
        public string templateHtml { get; set; }
        [Required]
        public DateTime createDate { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual RequirementTypeEntity RequirementType { get; set; }
    }
}
