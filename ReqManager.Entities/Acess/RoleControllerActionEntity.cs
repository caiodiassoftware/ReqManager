using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Acess
{
    public class RoleControllerActionEntity
    {
        [Key]
        public int RoleControllerActionID { get; set; }
        [Required]
        public int RoleID { get; set; }
        [Required]
        public int ControllerActionID { get; set; }
    }
}
