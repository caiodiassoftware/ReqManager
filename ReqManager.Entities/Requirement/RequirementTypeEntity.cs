using System;
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
        [Display(Name = "Type Description")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string description { get; set; }
        [MaxLength(4)]
        [Required]
        [Display(Name = "Abbreviation")]
        public string abbreviation { get; set; }

        public String DisplayName
        {
            get
            {
                return this.abbreviation + " - " + this.description;
            }
        }
    }
}
