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
        public int HistoryArtefactID { get; set; }
        [Required]
        public int ProjectArtifactID { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionTypeArtifact { get; set; }
        [Required]
        [MaxLength(50)]
        public string descriptionMeasureImportance { get; set; }
        [Required]
        [MaxLength(300)]
        public string path { get; set; }
        [Required]
        [MaxLength(500)]
        public string description { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25), MinLength(5)]
        public string login { get; set; }

        public virtual ProjectArtifactEntity ProjectArtifact { get; set; }
    }
}
