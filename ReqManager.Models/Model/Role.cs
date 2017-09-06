using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Models
{
    [Table("ROLE", Schema = "ACESS")]
    public class Role
    {
        [Key]
        public int ID_role { get; set; }
        [Required]
        [MaxLength(50)]
        public string description { get; set; }

        public virtual ICollection<RoleControllerAction> roleControllerActions { get; set; }
        public virtual ICollection<UserRole> userRoles { get; set; }
    }
}
