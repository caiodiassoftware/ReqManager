using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Model
{
    [Table("SUBTASK", Schema = "TASK")]
    public class Subtask
    {
        [Key]
        public int SubtaskID { get; set; }
        public int StatusTaskID { get; set; }
        public int TaskTypeID { get; set; }
        public int UserTaskID { get; set; }
        [Required]
        public System.DateTime creationDate { get; set; }
        [MinLength(3)]
        [MaxLength(1000)]
        [Required]
        public string description { get; set; }
        public System.DateTime startDate { get; set; }
        public Nullable<DateTime> endDate { get; set; }
            
        public virtual StatusTask StatusTask { get; set; }
        public virtual TaskType TaskType { get; set; }
        public virtual UserTask UserTask { get; set; }
    }
}
