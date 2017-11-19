using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUIREMENT_RATIONALE", Schema = "REQ")]
    public partial class RequirementRationale
    {
        [Key]
        public int RequirementRationaleID { get; set; }
        public int RequirementID { get; set; }
        [Required]
        [MaxLength(100), MinLength(10)]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionType { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionSubType { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionStakeholder { get; set; }
        [Required]
        [MaxLength(50)]
        public string importance { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionStatus { get; set; }
        [Required]
        public DateTime changedDate { get; set; }
        [Required]
        [MaxLength(1000), MinLength(10)]
        public string rationale { get; set; }
    }
}
