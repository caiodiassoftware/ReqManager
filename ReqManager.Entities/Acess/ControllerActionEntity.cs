using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities
{
    public class ControllerActionEntity
    {
        [Key]
        [Display(Name = "Controller/Action")]
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

        public String DisplayName
        {
            get
            {
                return this.controller + "  /  " + action + "  -  " + (IsGet ? "HttpGet" : "HttpPost");
            }
        }
    }
}
