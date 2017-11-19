using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("TASK", Schema = "TASK")]
    public class Task
    {
        public Task()
        {
            this.HistoryTask = new HashSet<HistoryTask>();
            this.UserTask = new HashSet<UserTask>();
        }
    
        [Key]
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public int ProjectRequirementID { get; set; }
        public int MeasureImportanceID { get; set; }
        public int StatusTaskID { get; set; }
        public int TaskTypeTemplateID { get; set; }
        public int TaskTypeID { get; set; }
        [Required]
        public System.DateTime creationDate { get; set; }
        [MinLength(3)]
        [MaxLength(1000)]
        [Required]
        public string description { get; set; }
        [Required]
        public DateTime startDate { get; set; }
        [MaxLength(25)]
        [Index(IsUnique = true)]
        public string code { get; set; }
        public Nullable<DateTime> endDate { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual ProjectRequirements ProjectRequirements { get; set; }
        public virtual Importance MeasureImportance { get; set; }
        public virtual ICollection<HistoryTask> HistoryTask { get; set; }
        public virtual StatusTask StatusTask { get; set; }
        public virtual TaskTypeTemplate TaskTypeTemplate { get; set; }
        public virtual TaskType TaskType { get; set; }
        public virtual ICollection<UserTask> UserTask { get; set; }
    }
}
