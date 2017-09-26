using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Task
{
    public class TaskTypeTemplateEntity
    {
        [Key]
        public int TaskTypeTemplateID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int TaskTypeID { get; set; }
        [Required]
        [MaxLength(1000)]
        [Display(Name = "Template Html")]
        public string templateHtml { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual TaskTypeEntity TaskType { get; set; }
    }
}
