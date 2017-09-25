using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Task
{
    public class SubtaskEntity
    {
        [Key]
        public int SubtaskID { get; set; }
        [Required]
        public int StatusTaskID { get; set; }
        [Required]
        public int TaskTypeID { get; set; }
        [Required]
        public int UserTaskID { get; set; }
        [Required]
        public System.DateTime creationDate { get; set; }
        [MinLength(3)]
        [MaxLength(1000)]
        [Required]
        public string description { get; set; }
        [Required]
        public DateTime startDate { get; set; }
        public Nullable<DateTime> endDate { get; set; }

        public virtual StatusTaskEntity StatusTask { get; set; }
        public virtual TaskTypeEntity TaskType { get; set; }
        public virtual UserTaskEntity UserTask { get; set; }
    }
}
