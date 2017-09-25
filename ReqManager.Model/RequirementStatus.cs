using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("REQUIREMENT_STATUS", Schema = "REQ")]
    public partial class RequirementStatus
    {
        public RequirementStatus()
        {
            this.Requirement = new HashSet<Requirement>();
        }
    
        [Key]
        public int RequirementStatusID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Index(IsUnique = true)]
        public string description { get; set; }
    
        public virtual ICollection<Requirement> Requirement { get; set; }
    }
}
