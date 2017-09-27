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
        [Display(Name = "Status")]
        public int StatusTaskID { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int TaskTypeID { get; set; }
        [Required]
        [Display(Name = "Task-User")]
        public int UserTaskID { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        public System.DateTime creationDate { get; set; }
        [MinLength(3)]
        [MaxLength(1000)]
        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime startDate { get; set; }
        [Display(Name = "End Date")]
        public Nullable<DateTime> endDate { get; set; }

        public String DisplayName
        {
            get
            {
                return this.UserTask.Task.DisplayName + "." + this.SubtaskID;
            }
        }

        public virtual StatusTaskEntity StatusTask { get; set; }
        public virtual TaskTypeEntity TaskType { get; set; }
        public virtual UserTaskEntity UserTask { get; set; }
    }
}
