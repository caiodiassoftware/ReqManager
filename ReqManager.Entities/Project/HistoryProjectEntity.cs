using ReqManager.Entities.Acess;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class HistoryProjectEntity
    {
        [Key]
        [Display(Name = "Project History")]
        public int HistoryProjectID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public int CreationUserID { get; set; }
        [Required]
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Phases")]
        public string descriptionPhases { get; set; }
        [Display(Name = "Start Date")]
        public DateTime startDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime endDate { get; set; }
        [Display(Name = "Changed Date")]
        public DateTime changedDate { get; set; } = DateTime.Now;

        public virtual UserEntity Users { get; set; }
        public virtual ProjectEntity Project { get; set; }
    }
}
