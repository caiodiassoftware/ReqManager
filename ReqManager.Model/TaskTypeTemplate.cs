using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("TASK_TYPE_TEMPLATE", Schema = "TASK")]
    public class TaskTypeTemplate
    {
        public TaskTypeTemplate()
        {
            this.Task = new HashSet<Task>();
        }
    
        [Key]
        public int TaskTypeTemplateID { get; set; }
        public int CreationUserID { get; set; }
        public int TaskTypeID { get; set; }
        [Required]
        [MaxLength(1000)]
        public string templateHtml { get; set; }
        [Required]
        public DateTime creationDate { get; set; }

        [ForeignKey("CreationUserID")]
        public virtual Users Users { get; set; }
        public virtual ICollection<Task> Task { get; set; }
        public virtual TaskType TaskType { get; set; }
    }
}
