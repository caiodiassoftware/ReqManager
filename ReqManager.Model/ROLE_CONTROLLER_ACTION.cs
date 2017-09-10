using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("ROLE_CONTROLLER_ACTION", Schema = "ACESS")]
    public class ROLE_CONTROLLER_ACTION
    {
        [Key]
        public int RoleControllerActionID { get; set; }
    
        public virtual CONTROLLER_ACTION ControllerAction { get; set; }
        public virtual ROLE Role { get; set; }
    }
}
