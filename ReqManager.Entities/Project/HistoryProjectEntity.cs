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
        public int UserID { get; set; }
        [Required]
        public int ProjectID { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionPhases { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime changedDate { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual ProjectEntity Project { get; set; }
    }
}
