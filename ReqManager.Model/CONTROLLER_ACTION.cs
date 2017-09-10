using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("CONTROLLER_ACTION", Schema = "ACESS")]
    public class CONTROLLER_ACTION
    {
        public CONTROLLER_ACTION()
        {
            this.roleControllerAction = new HashSet<ROLE_CONTROLLER_ACTION>();
        }
    
        [Key]
        public int ControllerActionID { get; set; }
        [Required]
        [MaxLength(100)]
        public string controller { get; set; }
        [Required]
        [MaxLength(255)]
        public string action { get; set; }
    
        public virtual ICollection<ROLE_CONTROLLER_ACTION> roleControllerAction { get; set; }
    }
}
