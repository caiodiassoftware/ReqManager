using ReqManager.Entities.Project;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementCharacteristicsEntity
    {
        [Key]
        public int RequirementCharacteristicsID { get; set; }
        [Required]
        [Display(Name = "Characteristics")]
        public int CharacteristicsID { get; set; }
        [Required]
        [Display(Name = "Requirement")]
        public int RequirementID { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool active { get; set; }

        public virtual CharacteristicsEntity Characteristics { get; set; }
        public virtual RequirementEntity Requirement { get; set; }
    }
}
