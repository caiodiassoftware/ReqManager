using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Acess
{
    public class SubtaskEntity
    {
        [Key]
        public int SubtaskID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Status")]
        public int StatusTaskID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Type")]
        public int TaskTypeID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Task-User")]
        public int UserTaskID { get; set; }
        [Display(Name = "Creation Date")]
        public System.DateTime creationDate { get; set; } = DateTime.Now;
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
        [Display(Name = "Subtask Code")]
        public string code { get; set; }

        public String DisplayName
        {
            get
            {
                return this.UserTask.Task.DisplayName + "." + this.code;
            }
        }

        public virtual StatusTaskEntity StatusTask { get; set; }
        public virtual TaskTypeEntity TaskType { get; set; }
        public virtual UserTaskEntity UserTask { get; set; }
    }
}
