using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("ROLE_CONTROLLER_ACTION", Schema = "ACESS")]
    public class RoleControllerAction
    {
        [Key]
        public int RoleControllerActionID { get; set; }
    
        public virtual ControllerAction ControllerAction { get; set; }
        public virtual Role Role { get; set; }
    }
}
