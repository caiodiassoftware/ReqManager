using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("USER_ROLE", Schema = "ACESS")]
    public class UserRole
    {
        [Key]
        public int UserRoleID { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }

        public virtual Role Role { get; set; }
        public virtual Users User { get; set; }
    }
}
