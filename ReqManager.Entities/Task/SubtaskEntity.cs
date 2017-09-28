using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Acess
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
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Date")]
        public DateTime startDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Date")]
        public Nullable<DateTime> endDate { get; set; }
        [Required]
        [MaxLength(25)]
        public string code { get; set; }

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
