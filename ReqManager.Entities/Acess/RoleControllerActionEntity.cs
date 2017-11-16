using System;
using System.ComponentModel.DataAnnotations;

namespace ReqManager.Entities.Acess
{
    public class RoleControllerActionEntity
    {
        [Key]
        [Display(Name = "Role - Controller/Action")]
        public int RoleControllerActionID { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        [Display(Name = "Role")]
        public int RoleID { get; set; }
        [Display(Name = "Controller/Action")]
        [Required(ErrorMessage = "This field is Required")]
        [Range(1, Double.PositiveInfinity)]
        public int ControllerActionID { get; set; }

        public String DisplayName
        {
            get
            {
                return this.Role.description + " - " + ControllerAction.controller + "/" + ControllerAction.action;
            }
        }

        public virtual ControllerActionEntity ControllerAction { get; set; }
        public virtual RoleEntity Role { get; set; }        
    }
}
