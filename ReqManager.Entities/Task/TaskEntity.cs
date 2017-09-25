using ReqManager.Entities.Acess;
using ReqManager.Entities.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Task
{
    public class TaskEntity
    {
        [Key]
        public int TaskID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ProjectRequirementID { get; set; }
        [Required]
        public int MeasureImportanceID { get; set; }
        [Required]
        public int StatusTaskID { get; set; }
        [Required]
        public int TaskTypeTemplateID { get; set; }
        [Required]
        public int TaskTypeID { get; set; }
        [Required]
        public System.DateTime creationDate { get; set; }
        [MinLength(3)]
        [MaxLength(1000)]
        [Required]
        public string description { get; set; }
        [Required]
        public DateTime startDate { get; set; }
        public Nullable<DateTime> endDate { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual ProjectRequirementsEntity ProjectRequirements { get; set; }
        public virtual MeasureImportanceEntity MeasureImportance { get; set; }
        public virtual StatusTaskEntity StatusTask { get; set; }
        public virtual TaskTypeTemplateEntity TaskTypeTemplate { get; set; }
        public virtual TaskTypeEntity TaskType { get; set; }
    }
}
