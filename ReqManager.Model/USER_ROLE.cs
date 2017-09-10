using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("USER_ROLE", Schema = "ACESS")]
    public class USER_ROLE
    {
        [Key]
        public int UserRoleID { get; set; }
    
        public virtual ROLE Role { get; set; }
        public virtual USERS User { get; set; }
    }
}
