using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Entities.Artifact
{
    public class HistoryProjectArtifactEntity
    {
        [Key]
        [Display(Name = "History Artifact")]
        public int HistoryArtefactID { get; set; }
        [Required]
        [Display(Name = "Artifact")]
        public int ProjectArtifactID { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Type Artifact")]
        public string descriptionTypeArtifact { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Measure Importance")]
        public string descriptionMeasureImportance { get; set; }
        [Required]
        [MaxLength(300)]
        [Display(Name = "Path")]
        public string path { get; set; }
        [Required]
        [MaxLength(500)]
        [Display(Name = "Artifact Description")]
        public string description { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime creationDate { get; set; } = DateTime.Now;
        [Required]
        [MaxLength(25), MinLength(5)]
        [Display(Name = "Login")]
        public string login { get; set; }

        public virtual ProjectArtifactEntity ProjectArtifact { get; set; }
    }
}
