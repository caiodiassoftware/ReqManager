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
        public int RoleControllerActionID { get; set; }
        [Required]
        [Display(Name = "Role")]
        public int RoleID { get; set; }
        [Display(Name = "Controller/Action")]
        [Required]
        public int ControllerActionID { get; set; }

        public virtual RoleEntity Roles { get; set; }
        public virtual ControllerActionEntity ControllerAciton { get; set; }
    }
}
