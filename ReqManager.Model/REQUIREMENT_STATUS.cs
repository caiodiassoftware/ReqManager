using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUIREMENT_STATUS", Schema = "REQ")]
    public partial class REQUIREMENT_STATUS
    {
        public REQUIREMENT_STATUS()
        {
            this.Requirement = new HashSet<REQUIREMENT>();
        }
    
        [Key]
        public int RequirementStatusID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string description { get; set; }
    
        public virtual ICollection<REQUIREMENT> Requirement { get; set; }
    }
}
