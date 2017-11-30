using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("USER_TASK", Schema = "TASK")]
    public class UserTask
    {
        [Key]
        public int UserTaskID { get; set; }
        [Index("IX_USER_TASK", 1, IsUnique = true)]
        public int UserID { get; set; }
        [Index("IX_USER_TASK", 2, IsUnique = true)]
        public int TaskID { get; set; }
        public DateTime creationDate { get; set; }
    
        public virtual Users User { get; set; }
        public virtual Task Task { get; set; }
    }
}
