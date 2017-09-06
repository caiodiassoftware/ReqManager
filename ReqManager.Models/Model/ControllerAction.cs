using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Models
{
    [Table("CONTROLLER_ACTION", Schema = "ACESS")]
    public class ControllerAction
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ID_controllerAction { get; set; }
        [Required]
        [MaxLength(50)]
        public string controller { get; set; }
        [Required]
        [MaxLength(255)]
        public string action { get; set; }

        public virtual ICollection<RoleControllerAction> roleControllerActions { get; set; }
    }
}
