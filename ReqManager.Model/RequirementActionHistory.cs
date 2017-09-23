using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUIREMENT_ACTION_HISTORY", Schema = "REQ")]
    public class RequirementActionHistory
    {
        [Key]
        public int RequirementActionHistoryID { get; set; }
        public int RequirementID { get; set; }
        [Required]
        [MaxLength(15), MinLength(5)]
        public string UserLogin { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string DescriptionStatus { get; set; }
        [Required]
        public DateTime changedDate { get; set; }

        public virtual Requirement Requirement { get; set; }
    }
}
