using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities
{
    public class ControllerActionEntity
    {
        [Key]
        public int ControllerActionID { get; set; }
        [Required]
        [MaxLength(100)]
        public string controller { get; set; }
        [Required]
        [MaxLength(255)]
        public string action { get; set; }
        [Required]
        public bool IsGet { get; set; }
    }
}
