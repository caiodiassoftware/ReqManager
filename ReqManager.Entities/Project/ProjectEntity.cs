using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Project
{
    public class ProjectEntity
    {
        [Key]
        public int ProjectID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ProjectPhasesID { get; set; }
        [Required]
        [MaxLength(255)]
        public string description { get; set; }
        [Required]
        [MaxLength(300)]
        public string pathForTraceability { get; set; }
        [Required]
        [MaxLength(1000)]
        public string environmentalInformation { get; set; }
        [Required]
        [MaxLength(1000)]
        public string managementInformation { get; set; }
        [Required]
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        [Required]
        public DateTime creationDate { get; set; }

        public virtual UserEntity Users { get; set; }
        public virtual ProjectPhasesEntity ProjectPhases { get; set; }
    }
}
