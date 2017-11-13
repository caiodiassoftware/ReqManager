using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class ProjectPhasesEntity
    {
        [Key]
        [Display(Name = "Project Phases")]
        public int ProjectPhasesID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Display(Name = "Phases Description")]
        public string description { get; set; }
    }
}
