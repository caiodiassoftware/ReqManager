using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Acess
{
    public class RoleControllerActionEntity
    {
        [Key]
        [Display(Name = "Role - Controller/Action")]
        public int RoleControllerActionID { get; set; }
        [Required]
        [Display(Name = "Role")]
        public int RoleID { get; set; }
        [Display(Name = "Controller/Action")]
        [Required]
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
