using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("STATUS_TASK", Schema = "TASK")]
    public class StatusTask
    {
        public StatusTask()
        {
            this.Task = new HashSet<Task>();
        }

        [Key]
        public int TaskStatusID { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string description { get; set; }
    
        public virtual ICollection<Task> Task { get; set; }
    }
}
