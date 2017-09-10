using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("TASK_TYPE_TEMPLATE", Schema = "TASK")]
    public class TASK_TYPE_TEMPLATE
    {
        public TASK_TYPE_TEMPLATE()
        {
            this.Task = new HashSet<TASK>();
        }
    
        [Key]
        public int TaskTypeTemplateID { get; set; }
        [Required]
        [MaxLength(1000)]
        public string templateHtml { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
    
        public virtual USERS Users { get; set; }
        public virtual ICollection<TASK> Task { get; set; }
        public virtual TASK_TYPE TaskType { get; set; }
    }
}
