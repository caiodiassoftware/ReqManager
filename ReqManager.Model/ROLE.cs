using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("ROLE", Schema = "ACESS")]
    public class Role
    {
        public Role()
        {
            this.RoleControllerAction = new HashSet<RoleControllerAction>();
            this.UserRole = new HashSet<UserRole>();
        }
    
        [Key]
        public int RoleID { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        public string description { get; set; }
    
        public virtual ICollection<RoleControllerAction> RoleControllerAction { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
