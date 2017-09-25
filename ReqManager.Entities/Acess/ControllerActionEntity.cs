using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities
{
    public class ControllerActionEntity
    {
        [Key]
        public int ControllerActionID { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Controller")]
        public string controller { get; set; }
        [Required]
        [MaxLength(255)]
        [Display(Name = "Action")]
        public string action { get; set; }
        [Required]
        [Display(Name = "HttpGet/HttpPost")]
        public bool IsGet { get; set; }
    }
}
