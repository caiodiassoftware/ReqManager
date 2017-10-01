using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Requirement
{
    public class RequirementStatusEntity
    {
        [Key]
        public int RequirementStatusID { get; set; }
        [Required]
        [MaxLength(50), MinLength(4)]
        [Display(Name = "Status")]
        public string description { get; set; }
    }
}
