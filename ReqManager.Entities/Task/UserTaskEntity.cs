using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Task
{
    public class UserTaskEntity
    {
        [Key]
        public int UserTaskID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int TaskID { get; set; }
        [Required]
        public DateTime creationDate { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual TaskEntity Task { get; set; }
    }
}
