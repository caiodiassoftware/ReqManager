using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("ROLE", Schema = "ACCESS")]
    public class Role
    {
        public Role()
        {
            this.RoleControllerActionModel = new HashSet<RoleControllerAction>();
            this.UserRoleModel = new HashSet<UserRole>();
        }
    
        [Key]
        public int RoleID { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string name { get; set; }
        [MinLength(5)]
        [MaxLength(255)]
        public string description { get; set; }

        public virtual ICollection<RoleControllerAction> RoleControllerActionModel { get; set; }
        public virtual ICollection<UserRole> UserRoleModel { get; set; }
    }
}
