using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Project
{
    public class ProjectPhasesEntity
    {
        [Key]
        public int ProjectPhasesID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Display(Name = "Phases Description")]
        public string description { get; set; }
    }
}
