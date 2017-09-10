using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("PROJECT_PHASES", Schema = "PROJ")]
    public partial class PROJECT_PHASES
    {
        public PROJECT_PHASES()
        {
            this.Project = new HashSet<PROJECT>();
        }
    
        [Key]
        public int ProjectPhasesID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string description { get; set; }
    
        public virtual ICollection<PROJECT> Project { get; set; }
    }
}
