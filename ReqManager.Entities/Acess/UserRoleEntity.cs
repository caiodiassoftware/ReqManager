using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Acess
{
    public class UserRoleEntity
    {
        [Key]
        public int UserRoleID { get; set; }
        [Required]
        [Display(Name = "Role")]
        public int RoleID { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }

        public String DisplayName
        {
            get
            {
                return this.Role.description + " - " + User.nickName;
            }
        }

        public virtual RoleEntity Role { get; set; }
        public virtual UserEntity User { get; set; }

    }
}
