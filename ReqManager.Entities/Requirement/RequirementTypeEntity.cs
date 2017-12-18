using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Requirement
{
    public class RequirementTypeEntity
    {
        [Key]
        [Display(Name = "Requirement Type")]
        public int RequirementTypeID { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        [Display(Name = "Requirement Type")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
    }
}
