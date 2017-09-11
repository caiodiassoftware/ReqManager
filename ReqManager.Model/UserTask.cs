using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("USER_TASK", Schema = "TASK")]
    public class UserTask
    {
        [Key]
        public int UserTaskID { get; set; }
        public DateTime creationDate { get; set; }
    
        public virtual Users User { get; set; }
        public virtual Task Task { get; set; }
    }
}
