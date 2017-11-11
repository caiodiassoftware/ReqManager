using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementStatusEntity
    {
        [Key]
        [Display(Name = "Requirement Status")]
        public int RequirementStatusID { get; set; }
        [Required]
        [MaxLength(50), MinLength(4)]
        [Display(Name = "Status")]
        public string description { get; set; }
    }
}
