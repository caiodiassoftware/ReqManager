using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Task
{
    public class UserRoleEntity
    {
        [Key]
        public int UserRoleID { get; set; }
        [Required]
        public int RoleID { get; set; }
        [Required]
        public int UserID { get; set; }
    }
}
