using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("STATUS_TASK", Schema = "TASK")]
    public class STATUS_TASK
    {
        public STATUS_TASK()
        {
            this.Task = new HashSet<TASK>();
        }

        [Key]
        public int TaskStatusID { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        public string description { get; set; }
    
        public virtual ICollection<TASK> Task { get; set; }
    }
}
