using ReqManager.Entities.Acess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Acess
{
    public class RoleEntity
    {
        [Key]
        public int RoleID { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        [Display(Name = "Role Description")]
        public string description { get; set; }

    }
}
