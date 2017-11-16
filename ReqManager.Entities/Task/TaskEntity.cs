using ReqManager.Entities.Acess;
using ReqManager.Entities.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Acess
{
    public class TaskEntity
    {
        [Key]
        public int TaskID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Requirement")]
        public int ProjectRequirementID { get; set; }
        [Required]
        [Display(Name = "Importance")]
        public int MeasureImportanceID { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int StatusTaskID { get; set; }
        [Required]
        [Display(Name = "Type Template")]
        public int TaskTypeTemplateID { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int TaskTypeID { get; set; }
        [Display(Name = "Creation Date")]
        public System.DateTime creationDate { get; set; } = DateTime.Now;
        [MinLength(3)]
        [MaxLength(1000)]
        [Required]
        [Display(Name = "Task Description")]
        public string description { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime startDate { get; set; }
        [Display(Name = "End Date")]
        public Nullable<DateTime> endDate { get; set; }
        [Display(Name = "Task Code")]
        public string code { get; set; }

        public String DisplayName
        {
            get
            {
                return this.description;
            }
        }

        public virtual UserEntity Users { get; set; }
        public virtual ProjectRequirementsEntity ProjectRequirements { get; set; }
        public virtual MeasureImportanceEntity MeasureImportance { get; set; }
        public virtual StatusTaskEntity StatusTask { get; set; }
        public virtual TaskTypeTemplateEntity TaskTypeTemplate { get; set; }
        public virtual TaskTypeEntity TaskType { get; set; }
    }
}
