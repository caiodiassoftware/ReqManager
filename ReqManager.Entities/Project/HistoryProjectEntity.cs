using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Project
{
    public class HistoryProjectEntity
    {
        [Key]
        public int HistoryProjectID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public int UserID { get; set; }
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
        [Display(Name = "Description")]
        public DateTime changedDate { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual ProjectEntity Project { get; set; }
    }
}
