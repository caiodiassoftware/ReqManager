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
        [Index("IX_ROLE_CONTROLLER_ACTION", 1, IsUnique = true)]
        public int RoleID { get; set; }
        [Index("IX_ROLE_CONTROLLER_ACTION", 2, IsUnique = true)]
        public int ControllerActionID { get; set; }

        public virtual ControllerAction ControllerAction { get; set; }
        public virtual Role Role { get; set; }
    }
}
