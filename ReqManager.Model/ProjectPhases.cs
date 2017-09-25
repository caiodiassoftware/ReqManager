using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("PROJECT_PHASES", Schema = "PROJ")]
    public partial class ProjectPhases
    {
        public ProjectPhases()
        {
            this.Project = new HashSet<Project>();
        }
    
        [Key]
        public int ProjectPhasesID { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        [Index(IsUnique = true)]
        public string description { get; set; }
    
        public virtual ICollection<Project> Project { get; set; }
    }
}
