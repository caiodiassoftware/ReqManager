using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Models
{
    [Table("USER_ROLE", Schema = "ACESS")]
    public class UserRole
    {
        public UserRole()
        {
            users = new List<User>();
            roles = new List<Role>();
        }

        [Key]
        public int ID_userRole { get; set; }
        public int ID_user { get; set; }
        public int ID_role { get; set; }

        [ForeignKey("ID_user")]
        public virtual ICollection<User> users { get; set; }
        [ForeignKey("ID_role")]
        public virtual ICollection<Role> roles { get; set; }
    }
}
