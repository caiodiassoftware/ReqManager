using ReqManager.Entities.Acess;
using ReqManager.Entities.Requirement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Project
{
    public class ProjectRequirementsEntity
    {
        [Key]
        public int ProjectRequirementID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        [Required]
        [Display(Name = "Requirement")]
        public int RequirementID { get; set; }
        [Required]
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; }
        [Required]
        [Display(Name = "Traceable")]
        public bool traceable { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual ProjectEntity Project { get; set; }
        public virtual RequirementEntity Requirement { get; set; }
    }
}
