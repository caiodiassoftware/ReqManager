using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementActionHistoryEntity
    {
        [Key]
        [Display(Name = "Requirement History")]
        public int RequirementActionHistoryID { get; set; }
        [Required]
        [Display(Name = "Requirement")]
        public int RequirementID { get; set; }
        [Required]
        [MaxLength(15), MinLength(5)]
        [Display(Name = "Login")]
        public string UserLogin { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Display(Name = "Status")]
        public string DescriptionStatus { get; set; }
        [Display(Name = "Changed Date")]
        public DateTime changedDate { get; set; } = DateTime.Now;

        public virtual RequirementEntity Requirement { get; set; }
    }
}
