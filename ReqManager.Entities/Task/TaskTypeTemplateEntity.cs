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
        public int UserID { get; set; }
        [Required]
        public int TaskTypeID { get; set; }
        [Required]
        [MaxLength(1000)]
        public string templateHtml { get; set; }
        [Required]
        public DateTime creationDate { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual TaskTypeEntity TaskType { get; set; }
    }
}
