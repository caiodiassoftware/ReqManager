using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("ROLE", Schema = "ACESS")]
    public class ROLE
    {
        public ROLE()
        {
            this.RoleControllerAction = new HashSet<ROLE_CONTROLLER_ACTION>();
            this.UserRole = new HashSet<USER_ROLE>();
        }
    
        [Key]
        public int RoleID { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        public string description { get; set; }
    
        public virtual ICollection<ROLE_CONTROLLER_ACTION> RoleControllerAction { get; set; }
        public virtual ICollection<USER_ROLE> UserRole { get; set; }
    }
}
