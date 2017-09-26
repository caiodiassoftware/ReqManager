using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Requirement
{
    public class RequirementActionHistoryEntity
    {
        [Key]
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
        [Required]
        [Display(Name = "Changed Date")]
        public DateTime changedDate { get; set; }

        public virtual RequirementEntity Requirement { get; set; }
    }
}
