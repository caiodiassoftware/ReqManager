using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Acess
{
    public class HistoryTaskEntity
    {
        [Key]
        public int HistoryTaskID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int CreationUserID { get; set; }
        [Required]
        [Display(Name = "Task")]
        public int TaskID { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime startDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime endDate { get; set; }
        [Required]
        [MaxLength(1000), MinLength(5)]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Display(Name = "Importance")]
        public string descriptionImportance { get; set; }
        [Display(Name = "Changed Date")]
        public DateTime changedDate { get; set; } = DateTime.Now;

        public virtual UserEntity Users { get; set; }
        public virtual TaskEntity Task { get; set; }
    }
}
