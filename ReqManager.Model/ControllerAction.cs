using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("CONTROLLER_ACTION", Schema = "ACESS")]
    public class ControllerAction
    {
        public ControllerAction()
        {
            this.roleControllerAction = new HashSet<RoleControllerAction>();
        }
    
        [Key]
        public int ControllerActionID { get; set; }
        [Required]
        [MaxLength(100)]
        public string controller { get; set; }
        [Required]
        [MaxLength(255)]
        public string action { get; set; }
    
        public virtual ICollection<RoleControllerAction> roleControllerAction { get; set; }
    }
}
