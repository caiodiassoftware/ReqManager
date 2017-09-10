using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReqManager.Model
{
    [Table("HISTORY_PROJECT_ARTIFACT", Schema = "ART")]
    public class HISTORY_PROJECT_ARTIFACT
    {
        [Key]
        public int HistoryArtefactID { get; set; }
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
        public System.DateTime creationDate { get; set; }
        [Required]
        [MaxLength(25), MinLength(5)]
        public string login { get; set; }
    
        public virtual PROJECT_ARTIFACT ProjectArtifact { get; set; }
    }
}
