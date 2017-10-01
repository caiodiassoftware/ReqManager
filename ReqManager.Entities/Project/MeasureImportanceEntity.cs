using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Project
{
    public class MeasureImportanceEntity
    {
        [Key]
        public int MeasureImportanceID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Display(Name = "Measure Importance")]
        public string description { get; set; }
    }
}
